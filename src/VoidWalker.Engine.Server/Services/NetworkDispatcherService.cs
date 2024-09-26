using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Server.Services;

public class NetworkDispatcherService : BaseVoidWalkerService, INetworkDispatcherService
{
    public NetworkDispatcherService(ILogger<NetworkDispatcherService> logger) : base(logger)
    {
    }

    public Task DispatchMessageAsync<TEntity>(string sessionId, TEntity packetData) where TEntity : INetworkPacket
    {
        return Task.CompletedTask;
    }
}
