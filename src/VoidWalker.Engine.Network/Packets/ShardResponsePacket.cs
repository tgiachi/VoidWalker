using VoidWalker.Engine.Core.Data.Shared;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Network.Packets;

public record ShardResponsePacket(string Token, DateTime ExpireDateTime, List<ShardObject> Shards) : INetworkPacket;
