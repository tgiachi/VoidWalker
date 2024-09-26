using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Data.Internal;
using VoidWalker.Engine.Core.Interfaces.Dispatcher;
using VoidWalker.Engine.Network.Interfaces;

namespace VoidWalker.Engine.Core.Extensions;

public static class RegisterNetworkDispatcherExtension
{
    public static IServiceCollection RegisterNetworkDispatcher<TEntity, TListener>(this IServiceCollection services)
        where TEntity : INetworkPacket
        where TListener : INetworkDispatcherListener
    {
        services.AddToRegisterTypedList(new NetworkDispatcherData(typeof(TEntity), typeof(TListener)));

        services.AddSingleton(typeof(TListener));
        return services;
    }
}
