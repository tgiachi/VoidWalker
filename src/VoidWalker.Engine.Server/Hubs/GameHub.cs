using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using VoidWalker.Engine.Core.Data.Events.Player;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Network;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Network.Interfaces;


namespace VoidWalker.Engine.Server.Hubs;

public class GameHub : Hub
{
    private readonly IMessageBusService _mediator;

    private readonly ILogger _logger;

    public GameHub(IMessageBusService mediator, ILogger<GameHub> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task ReceiveMessage(NetworkPacketData message)
    {
        _logger.LogInformation("Received message from {ConnectionId}: {Message}", Context.ConnectionId, message.PacketType);

        DeserializeAndCreatePacket(message, Context.ConnectionId);
    }

    private void DeserializeAndCreatePacket(
        NetworkPacketData packet, string sessionId
    )
    {
        var packetType = Type.GetType(packet.PacketType);
        if (packetType == null)
        {
            throw new InvalidOperationException($"{packet.PacketType} is not a valid packet type.");
        }

        var obj = JsonSerializer.Deserialize(packet.PacketData, packetType);


        MethodInfo createMethod = GetType()
            .MakeGenericType(packetType)
            .GetMethod(nameof(SendPacketEvent), BindingFlags.Public | BindingFlags.Static);

        createMethod.Invoke(null, new[] { sessionId, obj, _mediator });
    }

    private static void SendPacketEvent<TMessage>(TMessage message, string sessionId, IMessageBusService messageBusService)
        where TMessage : INetworkPacket
    {
        var incomingPacket = IncomingNetworkPacket<TMessage>.Create(sessionId, message);

        messageBusService.Publish(incomingPacket);
    }

    public override async Task OnDisconnectedAsync(Exception? exception) =>
        _mediator.Publish(new PlayerDisconnectedEvent(Context.ConnectionId));

    public override async Task OnConnectedAsync() =>
        _mediator.Publish(new PlayerConnectedEvent(Context.ConnectionId));
}
