using Redbus.Events;

namespace VoidWalker.Engine.Core.Data.Events;

public class ShardOfflineEvent : EventBase
{
    public string Id { get; set; }

}
