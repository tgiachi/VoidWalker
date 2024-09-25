using VoidWalker.Engine.Core.Data.Events.Player;
using VoidWalker.Engine.Core.Interfaces.Services;

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
