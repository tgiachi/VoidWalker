using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Database.Context;
using VoidWalker.Engine.Database.Services;

namespace VoidWalker.Engine.Database.Extensions;

public static class RegisterDbMigrationMethodEx
{
    public static IServiceCollection AddDbMigrationService<TDbContext>(this IServiceCollection services)
        where TDbContext : BaseDbContext
    {
        services.AddHostedService<AutoMigrationHostedService<TDbContext>>();

        return services;
    }
}
