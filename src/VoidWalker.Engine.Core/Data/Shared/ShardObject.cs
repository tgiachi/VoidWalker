namespace VoidWalker.Engine.Core.Data.Shared;

public class ShardObject
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }

    public string Type { get; set; }


    public ShardObject(string name, string description, string icon, string type)
    {
        Name = name;
        Description = description;
        Icon = icon;
        Type = type;
    }

    public ShardObject()
    {
    }

    public override string ToString()
    {
        return $"Name: {Name}, Description: {Description}, Icon: {Icon}, Type: {Type}";
    }
}
