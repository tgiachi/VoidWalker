using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Network.Events;

public record SendListOutputEvent(string SessionId, List<NetworkPacketData> Data, bool IsBroadcast) : IVoidWalkerEvent;
