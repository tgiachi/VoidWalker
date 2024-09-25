using System.Text.Json;
using Microsoft.Extensions.Logging;
using VoidWalker.Engine.Core.Attributes.Scripts;
using VoidWalker.Engine.Core.Data.Json.TileSet;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Core.ScriptModules.Services;

[ScriptModule]
public class TileServiceModule
{
    private readonly ITileSetService _tileSetService;

    private readonly ILogger _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public TileServiceModule(
        ITileSetService tileSetService, JsonSerializerOptions jsonSerializerOptions, ILogger<TileServiceModule> logger
    )
    {
        _tileSetService = tileSetService;
        _jsonSerializerOptions = jsonSerializerOptions;
        _logger = logger;
    }


    [ScriptFunction("tiles_addTileSet")]
    public void AddTileSet(object obj)
    {
        var tileSetJson = JsonSerializer.Serialize(obj, _jsonSerializerOptions);

        var tileSet = JsonSerializer.Deserialize<TileSetObject>(tileSetJson, _jsonSerializerOptions);


        if (tileSet == null || string.IsNullOrWhiteSpace(tileSet.Name))
        {
            throw new Exception("Failed to deserialize tile set");
        }

        _logger.LogInformation("Adding tile set: {TileSetName}", tileSet.Name);
        _tileSetService.AddTileSet(tileSet);
    }

    [ScriptFunction("tiles_getTileSet")]
    public TileSetObject? GetTileSet(string name) => _tileSetService.GetTileSet(name);
}
