using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Packets;

public record LoginResponsePacket(bool IsSuccess, string Token, string SessionId) : INetworkPacket;
