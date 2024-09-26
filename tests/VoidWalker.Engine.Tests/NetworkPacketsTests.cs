using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Network.Data.Shared;
using VoidWalker.Engine.Network.Packets;

namespace VoidWalker.Engine.Tests;

public class NetworkPacketsTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void HelloRequest_Serialize()
    {
        var networkPacket = new HelloRequestPacket().ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(HelloRequestPacket)));
    }

    [Test]
    public void LoginRequest_Serialize()
    {
        var packet = new LoginRequestPacket("user", "pass");
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(LoginRequestPacket)));
    }

    [Test]
    public void LoginResponse_Serialize()
    {
        var packet = new LoginResponsePacket(true, "token", DateTime.Now, "sessionId");
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(LoginResponsePacket)));
    }

    [Test]
    public void ShardRequest_Serialize()
    {
        var packet = new ShardRequestPacket();
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(ShardRequestPacket)));
    }

    [Test]
    public void ShardResponse_Serialize()
    {
        var packet = new ShardResponsePacket(new List<ShardObject>());
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(ShardResponsePacket)));
    }

    [Test]
    public void VersionRequest_Serialize()
    {
        var packet = new VersionRequestPacket();
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(VersionRequestPacket)));
    }

    [Test]
    public void VersionResponse_Serialize()
    {
        var packet = new VersionResponsePacket("1.0.0");
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(VersionResponsePacket)));
    }

    [Test]
    public void HelloResponse_Serialize()
    {
        var packet = new HelloResponsePacket();
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(HelloResponsePacket)));
    }

    [Test]
    public void PongResponse_Serialize()
    {
        var packet = new PongResponsePacket();
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(PongResponsePacket)));
    }

    [Test]
    public void PingRequest_Serialize()
    {
        var packet = new PingRequestPacket();
        var networkPacket = packet.ToNetworkPacketData();

        Assert.That(networkPacket.PacketType, Is.EqualTo(nameof(PingRequestPacket)));
    }
}
