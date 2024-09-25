using Redbus;
using Redbus.Events;
using Redbus.Interfaces;
using VoidWalker.Engine.Core.Interfaces.Services;

namespace VoidWalker.Engine.Server.Services;

public class MessageBusService : IMessageBusService
{
    private readonly IEventBus _eventBus = new EventBus();

    public Task InitializeAsync() => Task.CompletedTask;

    public void Publish<T>(T message) where T : EventBase
    {
        _eventBus.PublishAsync(message);
    }


    public SubscriptionToken Subscribe<T>(Action<T> action) where T : EventBase => _eventBus.Subscribe(action);
}
