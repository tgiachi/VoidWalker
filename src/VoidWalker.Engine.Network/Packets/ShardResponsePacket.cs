using VoidWalker.Engine.Network.Data.Shared;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Packets;

public record ShardResponsePacket(List<ShardObject> Shards) : INetworkPacket;
