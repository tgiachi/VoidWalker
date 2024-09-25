using Redbus.Events;


namespace VoidWalker.Engine.Core.Data.Events.Player;

public class PlayerDisconnectedEvent(string SessionId) : EventBase
{
    public string SessionId { get; init; } = SessionId;
}
