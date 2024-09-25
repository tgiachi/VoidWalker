using Redbus.Events;


namespace VoidWalker.Engine.Core.Data.Events;

public class ShardOnlineEvent : EventBase
{

    public string Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

}
