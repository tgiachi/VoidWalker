using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using VoidWalker.Engine.Core.Data.Configs;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Core.Services;

public class RedisCacheService : IRedisCacheService
{
    private readonly ILogger _logger;
    private readonly RedisConnectionPoolManager _redisConnectionPool;
    private readonly RedisCredentials _redisCredentials;


    public RedisCacheService(ILogger<RedisCacheService> logger, IOptions<RedisCredentials> redisCredentials)
    {
        _logger = logger;
        _redisCredentials = redisCredentials.Value;

        _redisConnectionPool = new RedisConnectionPoolManager(
            new RedisConfiguration()
            {
                Password = redisCredentials.Value.Password,
                PoolSize = 10,
                ConnectTimeout = 2000,
                SyncTimeout = 3000,
                ConfigurationOptions =
                {
                    Password = redisCredentials.Value.Password,
                    EndPoints = { $"{redisCredentials.Value.Host}:{redisCredentials.Value.Port}" },
                    KeepAlive = -1
                }
            }
        );


        _logger.LogInformation(
            "Connecting to {Hostname}:{Port} with password",
            redisCredentials.Value.Host,
            redisCredentials.Value.Port
        );
    }


    public async Task<TEntity> SetCacheObjectAsync<TEntity>(string keyName, TEntity entity, int ttl = 60)
    {
        _logger.LogDebug("Setting cache of {Ttl} seconds for type: {Type}", ttl, typeof(TEntity).Name);
        var db = _redisConnectionPool.GetConnection().GetDatabase();
        if (typeof(TEntity) == typeof(string))
        {
            await db.StringSetAsync(keyName, entity as string, new TimeSpan(0, 0, ttl));
        }
        else
        {
            await db.StringSetAsync(
                keyName,
                JsonSerializer.Serialize(entity, AddDefaultJsonSettingsExtension.GetDefaultJsonSettings()),
                new TimeSpan(0, 0, ttl)
            );
        }

        return entity;
    }

    public async Task<string> GetCacheStringObjectAsync(string keyName)
    {
        _logger.LogDebug("Getting cache of  type: {Type}", typeof(string).Name);

        var db = _redisConnectionPool.GetConnection().GetDatabase();

        var objStr = await db.StringGetAsync(keyName);

        if (objStr.IsNullOrEmpty)
        {
            return string.Empty;
        }

        return objStr.ToString();
    }

    public async Task<TEntity> GetCacheObjectAsync<TEntity>(string keyName)
    {
        _logger.LogDebug("Getting cache of type: {Type}", typeof(TEntity).Name);

        var db = _redisConnectionPool.GetConnection().GetDatabase();

        var objStr = await db.StringGetAsync(keyName);

        return objStr.IsNullOrEmpty
            ? default
            : JsonSerializer.Deserialize<TEntity>(
                objStr.ToString(),
                AddDefaultJsonSettingsExtension.GetDefaultJsonSettings()
            );
    }

    public async Task<List<TEntity>> GetCacheKeysAsync<TEntity>(string prefix)
    {
        _logger.LogDebug("Getting cache of type: {Type} with prefix {Prefix}", typeof(TEntity).Name, prefix);

        var db = _redisConnectionPool.GetConnection().GetDatabase();

        var keys = _redisConnectionPool.GetConnection()
            .GetServer($"{_redisCredentials.Host}:{_redisCredentials.Port}")
            .Keys(pattern: prefix);

        var results = keys
            .Select(
                k =>
                {
                    try
                    {
                        return JsonSerializer.Deserialize<TEntity>(
                            db.StringGet(k).ToString(),
                            AddDefaultJsonSettingsExtension.GetDefaultJsonSettings()
                        );
                    }
                    catch (Exception)
                    {
                        return default;
                    }
                }
            )
            .Where(s => s != null)
            .ToList();

        return results;
    }

    public async Task<bool> DeleteCacheObjectAsync(string keyName)
    {
        _logger.LogDebug("Deleting cache of  type: {KeyName}", keyName);

        var db = _redisConnectionPool.GetConnection().GetDatabase();
        return await db.KeyDeleteAsync(keyName);
    }

    public async Task SubscribeAsync<TEntity>(string channel, Action<RedisChannel, TEntity> handler) where TEntity : class
    {
        var sub = _redisConnectionPool.GetConnection().GetSubscriber();
        await sub.SubscribeAsync(
            channel,
            (redisChannel, value) =>
            {
                var obj = JsonSerializer.Deserialize<TEntity>(
                    value,
                    AddDefaultJsonSettingsExtension.GetDefaultJsonSettings()
                );
                handler(redisChannel, obj);
            }
        );
    }


    public async Task PublishAsync<TEntity>(string channel, TEntity message) where TEntity : class
    {
        var sub = _redisConnectionPool.GetConnection().GetSubscriber();
        await sub.PublishAsync(
            channel,
            JsonSerializer.Serialize(message, AddDefaultJsonSettingsExtension.GetDefaultJsonSettings())
        );
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
