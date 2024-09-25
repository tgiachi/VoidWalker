using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Interfaces.Services.Base;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Server.Hubs;

namespace VoidWalker.Engine.Server.Handlers;

public class OutputMessageEventHandler : IVoidWalkerService
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly ILogger _logger;
    private readonly IMessageBusService _messageBusService;

    public OutputMessageEventHandler(
        IHubContext<GameHub> hubContext, ILogger<OutputMessageEventHandler> logger, IMessageBusService messageBusService
    )
    {
        _hubContext = hubContext;
        _logger = logger;
        _messageBusService = messageBusService;

        _messageBusService.Subscribe<SendOutputEvent>(Handle);

        _messageBusService.Subscribe<SendListOutputEvent>(Handle);
    }


    public async void Handle(SendOutputEvent notification)
    {
        if (notification.IsBroadcast)
        {
            _logger.LogInformation("Broadcasting message: {Message}", notification.Data.PacketType);
            await _hubContext.Clients.All.SendAsync(
                "ReceiveMessage",
                notification.Data
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
                .SendAsync("ReceiveMessage", notification.Data);
        }
    }

    public async void Handle(SendListOutputEvent notification)
    {
        foreach (var packet in notification.Data)
        {
            Handle(new SendOutputEvent(notification.SessionId, packet, notification.IsBroadcast));
        }
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
