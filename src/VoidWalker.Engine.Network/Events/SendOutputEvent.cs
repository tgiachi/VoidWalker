using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Network.Events;

public record SendOutputEvent(string SessionId, NetworkPacketData Data) : IVoidWalkerEvent;
