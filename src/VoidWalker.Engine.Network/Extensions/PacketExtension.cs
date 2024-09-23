using System.Text.Json;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Extensions;

public static class PacketExtension
{
    public static NetworkPacketData ToNetworkPacketData(this INetworkPacket packet) =>
        new()
        {
            PacketType = packet.GetType().Name,
            PacketData = JsonSerializer.Serialize(packet, AddDefaultJsonSettingsExtension.GetDefaultJsonSettings())
        };


    public static INetworkPacket ToNetworkPacket(this NetworkPacketData packetData)
    {
        var packet = JsonSerializer.Deserialize(
            packetData.PacketData,
            Type.GetType(packetData.PacketType),
            AddDefaultJsonSettingsExtension.GetDefaultJsonSettings()
        );

        return (INetworkPacket)packet;
    }
}
