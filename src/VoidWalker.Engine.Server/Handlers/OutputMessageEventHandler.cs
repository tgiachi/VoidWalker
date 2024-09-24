using MediatR;
using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Server.Hubs;

namespace VoidWalker.Engine.Server.Handlers;

public class OutputMessageEventHandler : INotificationHandler<SendOutputEvent>, INotificationHandler<SendListOutputEvent>
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
            _logger.LogInformation("Broadcasting message: {Message}", notification.Data.PacketType);
            await _hubContext.Clients.All.SendAsync(
                "ReceiveMessage",
                notification.Data,
                cancellationToken: cancellationToken
            );
        }
        else
        {
            _logger.LogInformation(
                "Sending message to session: {SessionId} Type: {Type}",
                notification.SessionId,
                notification.Data.PacketType
            );
            await _hubContext.Clients.Client(notification.SessionId)
                .SendAsync("ReceiveMessage", notification.Data, cancellationToken: cancellationToken);
        }
    }

    public async Task Handle(SendListOutputEvent notification, CancellationToken cancellationToken)
    {
        foreach (var packet in notification.Data)
        {
            await Handle(new SendOutputEvent(notification.SessionId, packet, notification.IsBroadcast), cancellationToken);
        }
    }
}
