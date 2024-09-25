using Redbus.Events;


namespace VoidWalker.Engine.Network.Events;

public class SendOutputEvent(string SessionId, NetworkPacketData Data, bool IsBroadcast) : EventBase
{
    public string SessionId { get; init; } = SessionId;
    public NetworkPacketData Data { get; init; } = Data;
    public bool IsBroadcast { get; init; } = IsBroadcast;
}
