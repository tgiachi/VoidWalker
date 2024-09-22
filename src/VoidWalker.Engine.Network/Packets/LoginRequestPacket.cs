using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Packets;

public record LoginRequestPacket(string Username, string Password) : INetworkPacket;
