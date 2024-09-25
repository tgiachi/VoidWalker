using VoidWalker.Engine.Server.Data.Events;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Server.Handlers;

public class PlayerEventHandler
{
    private readonly ISessionService _sessionService;

    public PlayerEventHandler(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async ValueTask Handle(PlayerConnectedEvent notification, CancellationToken cancellationToken) =>
        await _sessionService.Handle(notification, cancellationToken);

    public async ValueTask Handle(PlayerDisconnectedEvent notification, CancellationToken cancellationToken) =>
        await _sessionService.Handle(notification, cancellationToken);
}
