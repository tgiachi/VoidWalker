using MediatR;
using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Server.Data.Events;

namespace VoidWalker.Engine.Server.Hubs;

public class GameHub : Hub
{
    private readonly IMediator _mediator;

    public GameHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await _mediator.Publish(new PlayerDisconnectedEvent(Context.ConnectionId));
    }

    public override async Task OnConnectedAsync()
    {
        await _mediator.Publish(new PlayerConnectedEvent(Context.ConnectionId));
    }
}
