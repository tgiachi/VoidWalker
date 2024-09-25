using StackExchange.Redis;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface IRedisCacheService : IVoidWalkerService
{
    Task<TEntity> SetCacheObjectAsync<TEntity>(string keyName, TEntity entity, int ttl = 60);

    Task<TEntity> GetCacheObjectAsync<TEntity>(string keyName);

    Task<string> GetCacheStringObjectAsync(string keyName);

    Task<List<TEntity>> GetCacheKeysAsync<TEntity>(string prefix);

    Task<bool> DeleteCacheObjectAsync(string keyName);

    Task SubscribeAsync<TEntity>(string channel, Action<RedisChannel, TEntity> handler) where TEntity : class;

    Task PublishAsync<TEntity>(string channel, TEntity message) where TEntity : class;

}
