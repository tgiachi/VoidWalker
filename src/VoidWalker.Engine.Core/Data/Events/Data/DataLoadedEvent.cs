using Redbus.Events;
using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Core.Data.Events.Data;

public class DataLoadedEvent(string Type, List<object> Data) : EventBase
{
    public string Type { get; init; } = Type;
    public List<object> Data { get; init; } = Data;


}
