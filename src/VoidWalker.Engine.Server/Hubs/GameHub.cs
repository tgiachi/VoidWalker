using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Server.Data.Events;
using Wolverine;

namespace VoidWalker.Engine.Server.Hubs;

public class GameHub : Hub
{
    private readonly IMessageBus _mediator;

    public GameHub(IMessageBus mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _mediator.PublishAsync(new PlayerDisconnectedEvent(Context.ConnectionId));
    }

    public override async Task OnConnectedAsync()
    {
        await _mediator.PublishAsync(new PlayerConnectedEvent(Context.ConnectionId));
    }
}
