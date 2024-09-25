using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Database.Data;
using VoidWalker.Engine.Database.Interfaces.Seed;

namespace VoidWalker.Engine.Database.Services;

public class AutoSeedHostedService : IHostedService
{
    private readonly List<SeedTypeData> _seedTypes;
    private readonly IServiceProvider _serviceProvider;

    private readonly ILogger _logger;

    public AutoSeedHostedService(
        List<SeedTypeData> seedTypes, IServiceProvider serviceProvider, ILogger<AutoSeedHostedService> logger
    )
    {
        _seedTypes = seedTypes;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting db seed service.");

        var scope = _serviceProvider.CreateAsyncScope();

        foreach (var seedType in _seedTypes)
        {
            var seed = (IDbSeed)scope.ServiceProvider.GetRequiredService(seedType.SeedType);
            await seed.SeedAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
