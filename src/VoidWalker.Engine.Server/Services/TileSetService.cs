using VoidWalker.Engine.Core.Data.Json.TileSet;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Server.Services;

public class TileSetService : BaseVoidWalkerService, ITileSetService
{
    public TileSetService(ILogger<TileSetService> logger, IDataLoaderService dataLoaderService) : base(logger)
    {
        dataLoaderService.SubscribeToDataLoaded<TileSetObject>("tile_set", OnTileSetLoaded);
    }

    private void OnTileSetLoaded(List<TileSetObject> obj)
    {
        Logger.LogInformation("TileSet loaded: {TileSetCount}", obj.Count);

        foreach (var tileSet in obj)
        {
            Logger.LogInformation("TileSet:  {TileSetName}", tileSet.Name);
        }
    }
}
