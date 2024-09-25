using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Core.Data.Events.Player;

public record PlayerConnectedEvent(string SessionId) : IVoidWalkerEvent;
