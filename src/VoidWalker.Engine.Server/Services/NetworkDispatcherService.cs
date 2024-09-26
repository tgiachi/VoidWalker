using VoidWalker.Engine.Core.Data.Internal;
using VoidWalker.Engine.Core.Interfaces.Dispatcher;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Services.Base;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Server.Services;

public class NetworkDispatcherService : BaseVoidWalkerService, INetworkDispatcherService
{
    private readonly Dictionary<Type, List<INetworkDispatcherListener>> _dispatcherCallbacks = new();

    private readonly IServiceProvider _serviceProvider;

    private readonly List<NetworkDispatcherData> _networkDispatcherData;

    public NetworkDispatcherService(
        ILogger<NetworkDispatcherService> logger, IServiceProvider serviceProvider,
        List<NetworkDispatcherData> networkDispatcherData
    ) : base(
        logger
    )
    {
        _serviceProvider = serviceProvider;
        _networkDispatcherData = networkDispatcherData;
    }

    public override Task InitializeAsync()
    {
        foreach (var dispatcherData in _networkDispatcherData)
        {
            var listener = (INetworkDispatcherListener)_serviceProvider.GetService(dispatcherData.ListenerType);
            if (listener == null)
            {
                Logger.LogError("Failed to get listener for {ListenerType}", dispatcherData.ListenerType);
                continue;
            }

            if (!_dispatcherCallbacks.ContainsKey(dispatcherData.PacketType))
            {
                _dispatcherCallbacks.Add(dispatcherData.PacketType, []);
            }

            _dispatcherCallbacks[dispatcherData.PacketType].Add(listener);
        }

        return Task.CompletedTask;
    }

    public async Task DispatchMessageAsync<TEntity>(string sessionId, TEntity packetData) where TEntity : INetworkPacket
    {
        if (!_dispatcherCallbacks.ContainsKey(typeof(TEntity)))
        {
            Logger.LogWarning("No listeners for packet type {PacketType}", typeof(TEntity));
            return;
        }

        var listeners = _dispatcherCallbacks[typeof(TEntity)];
        foreach (var listener in listeners)
        {
            var packets = await listener.OnDispatchMessageAsync(sessionId, packetData);
            foreach (var packet in packets)
            {
                //await NetworkManager.SendPacketAsync(sessionId, packet);
            }
        }
    }
}
