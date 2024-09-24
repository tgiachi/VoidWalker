using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Core.Data.Events;

public class ShardOfflineEvent : IVoidWalkerEvent
{
    public string Id { get; set; }

}
