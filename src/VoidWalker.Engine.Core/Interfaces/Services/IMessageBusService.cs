using Redbus;
using Redbus.Events;
using VoidWalker.Engine.Core.Interfaces.Events;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface IMessageBusService : IVoidWalkerService
{
    void Publish<T>(T message) where T : EventBase;


    SubscriptionToken Subscribe<T>(Action<T> action) where T : EventBase;
}
