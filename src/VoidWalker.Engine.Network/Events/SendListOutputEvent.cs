using Redbus.Events;


namespace VoidWalker.Engine.Network.Events;

public class SendListOutputEvent(string SessionId, List<NetworkPacketData> Data, bool IsBroadcast) : EventBase
{
    public string SessionId { get; } = SessionId;
    public List<NetworkPacketData> Data { get; } = Data;
    public bool IsBroadcast { get; } = IsBroadcast;
}
