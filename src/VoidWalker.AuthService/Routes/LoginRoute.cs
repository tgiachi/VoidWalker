using Microsoft.AspNetCore.Mvc;
using VoidWalker.AuthService.Interfaces;
using VoidWalker.Engine.Network.Packets;

namespace VoidWalker.AuthService.Routes;

public static class LoginRoute
{
    public static IEndpointRouteBuilder MapLoginRoute(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(
            "/auth/login",
            async (ILoginService loginService, [FromBody] LoginRequestPacket request) =>
            {
                var result = await loginService.LoginAsync(request.Username, request.Password);

                if (result.Success)
                {
                    return Results.Ok(
                        new LoginResponsePacket(true, result.Token, result.ExpiresAt.Value, Guid.NewGuid().ToString())
                    );
                }


                return Results.Unauthorized();
            }
        );


        return endpoints;
    }
}
