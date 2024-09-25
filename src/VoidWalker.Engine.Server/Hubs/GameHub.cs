using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Core.Data.Events.Player;
using VoidWalker.Engine.Core.Interfaces.Services;


namespace VoidWalker.Engine.Server.Hubs;

public class GameHub : Hub
{
    private readonly IMessageBusService _mediator;

    public GameHub(IMessageBusService mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _mediator.Publish(new PlayerDisconnectedEvent(Context.ConnectionId));
    }

    public override async Task OnConnectedAsync()
    {
         _mediator.Publish(new PlayerConnectedEvent(Context.ConnectionId));
    }
}
