namespace VoidWalker.Engine.Core.Data.Json.TileSet;

public class TileObject
{
    public string Id { get; set; }
    public int Position { get; set; }

    public override string ToString() => $"Id: {Id}, Position: {Position}";
}
