using VoidWalker.Engine.Core.Data.Events.Player;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface ISessionService : IVoidWalkerService
{
    Task Handle(PlayerConnectedEvent notification, CancellationToken cancellationToken);

    Task Handle(PlayerDisconnectedEvent notification, CancellationToken cancellationToken);
}
