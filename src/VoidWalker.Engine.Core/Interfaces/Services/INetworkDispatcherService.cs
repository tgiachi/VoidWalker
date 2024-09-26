using VoidWalker.Engine.Core.Interfaces.Services.Base;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface INetworkDispatcherService : IVoidWalkerService
{
    Task DispatchMessageAsync<TEntity>(string sessionId, TEntity packetData) where TEntity : INetworkPacket;
}
