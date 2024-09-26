using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Core.Interfaces.Dispatcher;

public interface INetworkDispatcherListener
{
    Task<IEnumerable<INetworkPacket>> OnDispatchMessageAsync(string sessionId, object packetData);
}
