namespace VoidWalker.Engine.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NetworkDispatcherListenerAttribute(Type packetType) : Attribute
{
    public Type PacketType { get; } = packetType;
}
