using VoidWalker.Engine.Core.Data.Json.TileSet;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Interfaces.Services;

public interface ITileSetService : IVoidWalkerService
{
    TileSetObject? GetTileSet(string name);

    Task<byte[]?> GetTileSetContentAsync(string name);


    void AddTileSet(TileSetObject tileSet);
}
