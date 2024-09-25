using Microsoft.AspNetCore.Mvc;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Server.Interfaces;

namespace VoidWalker.Engine.Server.Routes;

public static class TilesRoutes
{
    public static IEndpointRouteBuilder MapTilesRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(
            "/api/v1/tiles/definition/{id}",
            ([FromRoute] string id, ITileSetService tileSetService) =>
            {
                var tileSet = tileSetService.GetTileSet(id);

                return tileSet == null ? Results.NotFound() : Results.Ok(tileSet);
            }
        );


        endpoints.MapGet( // This is the endpoint that we are interested in
            "/api/v1/tiles/content/{id}",
            async ([FromRoute] string id, ITileSetService tileSetService) =>
            {
                var content = await tileSetService.GetTileSetContentAsync(id);

                // Return png

                return content == null ? Results.NotFound() : Results.File(content, "image/png");
            }
        );

        return endpoints;
    }
}
