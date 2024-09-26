using System.Collections;
using VoidWalker.Engine.Core.Interfaces.Dispatcher;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Core.Impl.Dispatcher;

public abstract class AbstractNetworkDispatcherListener<TMessage> : INetworkDispatcherListener
    where TMessage : INetworkPacket
{
    public Task<IEnumerable<INetworkPacket>> OnDispatchMessageAsync(string sessionId, object packetData) =>
        OnDispatchMessageAsync(sessionId, (TMessage)packetData);

    public virtual async Task<IEnumerable<INetworkPacket>> OnDispatchMessageAsync(string sessionId, TMessage packetData) =>
        Enumerable.Empty<INetworkPacket>();
}
