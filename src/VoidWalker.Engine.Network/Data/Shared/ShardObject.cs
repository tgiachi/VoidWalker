namespace VoidWalker.Engine.Network.Data.Shared;

public class ShardObject
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }

    public string Type { get; set; }

    public string Url { get; set; }


    public override string ToString()
    {
        return $"Name: {Name}, Description: {Description}, Icon: {Icon}, Type: {Type}";
    }
}
