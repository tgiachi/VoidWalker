
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Data.Events;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Server.Services;

public class SessionService
    : BaseVoidWalkerService, ISessionService
{
    public SessionService(ILogger<SessionService> logger) : base(logger)
    {
        Logger.LogInformation("Session service initialized.");
    }

    public Task Handle(PlayerConnectedEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Player connected: {SessionId}", notification.SessionId);
        return Task.CompletedTask;
    }

    public Task Handle(PlayerDisconnectedEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Player disconnected: {SessionId}", notification.SessionId);
        return Task.CompletedTask;
    }
}
