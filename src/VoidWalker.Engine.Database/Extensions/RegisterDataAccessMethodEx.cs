using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Database.Interfaces.Dao;

namespace VoidWalker.Engine.Database.Extensions;

public static class RegisterDataAccessMethodEx
{
    public static IServiceCollection RegisterDataAccess(this IServiceCollection services, Type dataAccessType)
    {
        services.AddScoped(typeof(IBaseDataAccess<>), dataAccessType);
        return services;
    }
}
