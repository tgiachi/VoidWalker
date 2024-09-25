using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Packets;

public record VersionResponsePacket(string Version) : INetworkPacket;
