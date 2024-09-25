
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Server.Data.Events;

namespace VoidWalker.Engine.Server.Interfaces;

public interface ISessionService : IVoidWalkerService
{
    Task Handle(PlayerConnectedEvent notification, CancellationToken cancellationToken);

    Task Handle(PlayerDisconnectedEvent notification, CancellationToken cancellationToken);
}
