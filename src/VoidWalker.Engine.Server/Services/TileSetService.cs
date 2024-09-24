using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Server.Services;

public class TileSetService : BaseVoidWalkerService, ITileSetService
{
    public TileSetService(ILogger<TileSetService> logger) : base(logger)
    {
    }
}
