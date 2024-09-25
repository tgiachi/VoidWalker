using VoidWalker.Engine.Core.Data.Events;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Server.Hosted;

public class GameServerHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IRedisCacheService _redisCacheService;

    private Guid _shardId = Guid.NewGuid();

    public GameServerHostedService(IRedisCacheService redisCacheService, ILogger<GameServerHostedService> logger)
    {
        _redisCacheService = redisCacheService;
        _logger = logger;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Broadcasting shard online event");
        await _redisCacheService.PublishAsync(
            "voidwalker_events",
            new ShardOnlineEvent
            {
                Id = _shardId.ToString(),
                Name = "ShardOnlineEvent",
                Url = "https://voidwalker.io"
            }
        );
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Broadcasting shard offline event");
        return _redisCacheService.PublishAsync(
            "voidwalker_events",
            new ShardOfflineEvent
            {
                Id = _shardId.ToString()
            }
        );
    }
}
