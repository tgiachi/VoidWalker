using VoidWalker.Engine.Core.Data.Events.Player;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Services.Base;

namespace VoidWalker.Engine.Server.Services;

public class SessionService
    : BaseVoidWalkerService, ISessionService
{
    private readonly IMessageBusService _messageBusService;

    public SessionService(ILogger<SessionService> logger, IMessageBusService messageBusService) : base(logger)
    {
        _messageBusService = messageBusService;
        _messageBusService.Subscribe<PlayerConnectedEvent>(Handle);
    }

    public void Handle(PlayerConnectedEvent notification)
    {
        Logger.LogInformation("Player connected: {SessionId}", notification.SessionId);
    }

    public void Handle(PlayerDisconnectedEvent notification)
    {
        Logger.LogInformation("Player disconnected: {SessionId}", notification.SessionId);
    }
}
