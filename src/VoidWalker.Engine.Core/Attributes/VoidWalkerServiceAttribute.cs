namespace VoidWalker.Engine.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class VoidWalkerServiceAttribute(int order, bool isAutoStart = true) : Attribute
{
    public int Order { get; set; } = order;

    public bool IsAutoStart { get; set; } = isAutoStart;
}
