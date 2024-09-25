using Redbus.Events;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Events;

public class IncomingNetworkPacket<TNetworkPacketData> : EventBase where TNetworkPacketData : INetworkPacket
{
    public string SessionId { get; init; }
    public TNetworkPacketData PacketData { get; init; }

    public static IncomingNetworkPacket<TNetworkPacketData> Create(string sessionId, TNetworkPacketData packetData) =>
        new()
        {
            SessionId = sessionId,
            PacketData = packetData
        };
}
