using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Attributes.Scripts;
using VoidWalker.Engine.Core.Data.Internal;

namespace VoidWalker.Engine.Core.Extensions;

public static class RegisterScriptClassExtension
{
    public static IServiceCollection RegisterScriptModule(this IServiceCollection services, Type classType)
    {
        var attribute = classType.GetCustomAttribute<ScriptModuleAttribute>();

        if (attribute == null)
        {
            throw new InvalidOperationException($"The class {classType.Name} is not a script module.");
        }

        services.AddSingleton(classType);

        services.AddToRegisterTypedList(new ScriptClassData(classType));


        return services;
    }

    public static IServiceCollection RegisterScriptModule<TClass>(this IServiceCollection services) =>
        services.RegisterScriptModule(typeof(TClass));
}
