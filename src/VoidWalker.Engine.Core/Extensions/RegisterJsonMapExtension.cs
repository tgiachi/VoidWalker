using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using VoidWalker.Engine.Core.Attributes.Json;
using VoidWalker.Engine.Core.Data.Internal;

namespace VoidWalker.Engine.Core.Extensions;

public static class RegisterJsonMapExtension
{
    public static void RegisterJsonMap<TJsonClass>(this IServiceCollection services) where TJsonClass : class
    {
        var attribute = typeof(TJsonClass).GetCustomAttribute<JsonTypeAttribute>();

        if (attribute == null)
        {
            throw new Exception($"JsonTypeAttribute not found on {typeof(TJsonClass).Name}");
        }

        services.AddToRegisterTypedList(new JsonMapTypeData(attribute.Name, typeof(TJsonClass)));
    }
}
