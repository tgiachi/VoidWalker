using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Data.Configs;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.Services;

namespace VoidWalker.Engine.Core.Extensions;

public static class RegisterRedisServiceExtension
{
    public static IServiceCollection RegisterRedis(this IServiceCollection services, IConfiguration configuration) =>
        services.RegisterConfig<RedisCredentials>(configuration, "Redis")
            .AddSingleton<IRedisCacheService, RedisCacheService>();
}
