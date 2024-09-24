using VoidWalker.Engine.Core.Attributes.Json;

namespace VoidWalker.Engine.Core.Data.Json.TileSet;

[JsonType("tile_set")]
public class TileSetObject
{
    public string Name { get; set; }

    public string Texture { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int TileWidth { get; set; }

    public int TileHeight { get; set; }

    public int Spacing { get; set; } = 0;

    public int Margin { get; set; } = 0;

    public List<TileObject> Tiles { get; set; } = new();
}
