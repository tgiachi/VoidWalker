using VoidWalker.AuthService.Interfaces;
using VoidWalker.Engine.Core.Data.Events;
using VoidWalker.Engine.Core.Data.Shared;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.AuthService.Service;

public class ShardService : IShardService
{
    private readonly ILogger _logger;

    private readonly IRedisCacheService _redisCacheService;

    private readonly List<ShardObject> _shards = new();

    public ShardService(ILogger<ShardService> logger, IRedisCacheService redisCacheService)
    {
        _logger = logger;
        _redisCacheService = redisCacheService;
    }

    public async Task InitializeAsync()
    {
        await _redisCacheService.SubscribeAsync<ShardOnlineEvent>(
            "voidwalker_events",
            async (channel, message) =>
            {
                _logger.LogInformation("Received shard online event");
                _shards.Add(new ShardObject
                {
                    Id = message.Id,
                    Name = message.Name,
                    Url = message.Url
                });

            }
        );
    }

    public Task<IEnumerable<ShardObject>> GetShardsAsync() => throw new NotImplementedException();
}
