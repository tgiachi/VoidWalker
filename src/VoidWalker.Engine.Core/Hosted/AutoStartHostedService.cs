using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Core.Data.Internal;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Hosted;

public class AutoStartHostedService : IHostedService
{
    private readonly ILogger _logger;

    private readonly List<VoidWalkerServiceData> _services;

    private readonly IServiceProvider _serviceProvider;

    public AutoStartHostedService(
        ILogger<AutoStartHostedService> logger, List<VoidWalkerServiceData> services, IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _services = services;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var service in _services.OrderBy(x => x.Order))
        {
            var serviceInstance = _serviceProvider.GetService(service.ServiceType);

            _logger.LogInformation("Starting service {Service}", service.ImplementationType.Name);

            if (serviceInstance is IVoidWalkerService voidWalkerService)
            {
                await voidWalkerService.InitializeAsync();
            }
            else
            {
                _logger.LogWarning(
                    "Service {Name} does not implement IVoidWalkerService, skipping start",
                    service.ImplementationType.Name
                );
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
