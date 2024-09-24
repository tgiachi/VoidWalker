using MediatR;
using VoidWalker.Engine.Server.Data.Events;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Server.Handlers;

public class PlayerEventHandler : INotificationHandler<PlayerConnectedEvent>, INotificationHandler<PlayerDisconnectedEvent>
{
    private readonly ISessionService _sessionService;

    public PlayerEventHandler(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public Task Handle(PlayerConnectedEvent notification, CancellationToken cancellationToken) =>
        _sessionService.Handle(notification, cancellationToken);

    public Task Handle(PlayerDisconnectedEvent notification, CancellationToken cancellationToken) =>
        _sessionService.Handle(notification, cancellationToken);
}
