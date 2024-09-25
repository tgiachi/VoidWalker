using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace VoidWalker.Engine.Core.Extensions;

public static class AddDefaultJsonSettingsExtension
{
    private static JsonSerializerOptions? _jsonSerializerOptions;

    public static IServiceCollection AddDefaultJsonSettings(this IServiceCollection services, bool formatted = true)
    {
        services.AddSingleton(GetDefaultJsonSettings(formatted));

        return services;
    }

    public static JsonSerializerOptions GetDefaultJsonSettings(bool formatted = true) =>
        _jsonSerializerOptions ??= new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            WriteIndented = formatted,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower) }
        };
}
