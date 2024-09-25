using VoidWalker.Engine.Core.Data.Json.TileSet;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Interfaces;
using VoidWalker.Engine.Server.Types;
using VoidWalker.Engine.Server.Utils;

namespace VoidWalker.Engine.Server.Services;

public class TileSetService : BaseVoidWalkerService, ITileSetService
{
    private readonly Dictionary<string, TileSetObject> _tileSets = new();

    public TileSetService(ILogger<TileSetService> logger, IDataLoaderService dataLoaderService) : base(logger)
    {
        dataLoaderService.SubscribeToDataLoaded<TileSetObject>("tile_set", OnTileSetLoaded);
    }

    private void OnTileSetLoaded(List<TileSetObject> obj)
    {
        Logger.LogInformation("TileSet loaded: {TileSetCount}", obj.Count);

        foreach (var tileSet in obj)
        {
            _tileSets[tileSet.Name] = tileSet;
        }
    }

    public TileSetObject? GetTileSet(string name) => _tileSets.GetValueOrDefault(name);

    public async Task<byte[]> GetTileSetContentAsync(string name)
    {
        var tileSet = GetTileSet(name);

        if (tileSet == null)
        {
            return null;
        }

        return await File.ReadAllBytesAsync(
            Path.Join(DirectoriesUtils.GetDirectoryPath(DirectoryType.Tiles), tileSet.Texture)
        );
    }

    public void AddTileSet(TileSetObject tileSet)
    {
        _tileSets[tileSet.Name] = tileSet;
    }
}
