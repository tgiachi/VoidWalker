using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Server.Data.Events;

public record PlayerConnectedEvent(string SessionId) : IVoidWalkerEvent;
