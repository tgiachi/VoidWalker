using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Core.Data.Events;

public class ShardOnlineEvent : IVoidWalkerEvent
{

    public string Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

}
