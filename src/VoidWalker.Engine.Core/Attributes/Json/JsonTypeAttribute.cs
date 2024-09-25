namespace VoidWalker.Engine.Core.Attributes.Json;

[AttributeUsage(AttributeTargets.Class)]
public class JsonTypeAttribute : Attribute
{
    public string Name { get; set; }

    public JsonTypeAttribute(string name)
    {
        Name = name;
    }
}
