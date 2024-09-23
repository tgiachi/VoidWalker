using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Database.Data;
using VoidWalker.Engine.Database.Interfaces.Seed;

namespace VoidWalker.Engine.Database.Extensions;

public static class RegisterSeedMethodEx
{
    public static IServiceCollection RegisterSeedService<TSeed>(this IServiceCollection services)
        where TSeed : IDbSeed
    {
        services.AddTransient(typeof(TSeed));

        services.AddToRegisterTypedList(new SeedTypeData(typeof(TSeed)));


        return services;
    }
}
