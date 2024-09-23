using MediatR;
using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Server.Hubs;

namespace VoidWalker.Engine.Server.Handlers;

public class OutputMessageEventHandler : INotificationHandler<SendOutputEvent>
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly ILogger _logger;

    public OutputMessageEventHandler(IHubContext<GameHub> hubContext, ILogger<OutputMessageEventHandler> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }


    public async Task Handle(SendOutputEvent notification, CancellationToken cancellationToken)
    {
        if (notification.IsBroadcast)
        {
            await _hubContext.Clients.All.SendAsync(
                "ReceiveMessage",
                notification.Data,
                cancellationToken: cancellationToken
            );
        }
        else
        {
            await _hubContext.Clients.Client(notification.SessionId)
                .SendAsync("ReceiveMessage", notification.Data, cancellationToken: cancellationToken);
        }
    }
}
