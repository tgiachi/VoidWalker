using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Exceptions;

namespace VoidWalker.Engine.Core.Extensions;

public static class RegisterConfigExtension
{
    public static IServiceCollection RegisterConfig<TConfig>(
        this IServiceCollection services, IConfiguration configuration, string sectionName
    )
        where TConfig : class
    {
        // var config = default(TConfig);
        // configuration.GetSection(sectionName).Bind(config);
        //
        //
        // if (config == null)
        // {
        //     throw new ConfigSectionNotFoundException(sectionName);
        // }


        services.AddOptions<TConfig>().BindConfiguration(sectionName);


        return services;
    }
}
