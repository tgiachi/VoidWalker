using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Data.Internal;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Interfaces.Services.Base;

namespace VoidWalker.Engine.Core.Extensions;

public static class RegisterVoidWalkerExtension
{
    public static IServiceCollection RegisterVoidWalkerService<TService>(
        this IServiceCollection services, bool autoStart = true, int order = 0
    ) where TService : class, IVoidWalkerService
    {
        services.AddSingleton<TService>();

        if (autoStart)
        {
            services.AddToRegisterTypedList(new VoidWalkerServiceData(typeof(TService), typeof(TService), order));
        }

        return services;
    }


    public static IServiceCollection RegisterVoidWalkerService<TInterface, TService>(
        this IServiceCollection services, bool autoStart = true, int order = 0
    ) where TService : class, TInterface, IVoidWalkerService where TInterface : class
    {
        services.AddSingleton<TInterface, TService>();

        if (autoStart)
        {
            services.AddToRegisterTypedList(new VoidWalkerServiceData(typeof(TInterface), typeof(TService), order));
        }

        return services;
    }
}
