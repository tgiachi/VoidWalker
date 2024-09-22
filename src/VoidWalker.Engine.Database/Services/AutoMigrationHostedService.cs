using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Database.Context;

namespace VoidWalker.Engine.Database.Services;

public class AutoMigrationHostedService<TDbContext> : IHostedService where TDbContext : BaseDbContext
{
    private readonly IDbContextFactory<TDbContext> _contextFactory;

    private readonly ILogger _logger;

    public AutoMigrationHostedService(
        IDbContextFactory<TDbContext> contextFactory, ILogger<AutoMigrationHostedService<TDbContext>> logger
    )
    {
        _contextFactory = contextFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("AutoMigration started");
        using var context = _contextFactory.CreateDbContext();

        if (context.Database.GetPendingMigrations().Any())
        {
            _logger.LogInformation("Migrating database");
            context.Database.Migrate();
        }

        _logger.LogInformation("AutoMigration finished");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
