using System.Reflection;
using VoidWalker.Engine.Network.Packets;

namespace VoidWalker.AuthService.Routes;

public static class VersionRoute
{
    public static IEndpointRouteBuilder MapVersionRoute(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/version",
            () =>
            {
                // get version from  assembly
                var version = Assembly.GetEntryAssembly()
                    ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    ?.InformationalVersion;


                return new VersionResponsePacket(version);
            }
        );

        return endpoints;
    }
}
