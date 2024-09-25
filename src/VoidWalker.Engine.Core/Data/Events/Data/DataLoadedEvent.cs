using VoidWalker.Engine.Core.Interfaces.Events;

namespace VoidWalker.Engine.Core.Data.Events.Data;

public record DataLoadedEvent(string Type, List<object> Data) : IVoidWalkerEvent;
