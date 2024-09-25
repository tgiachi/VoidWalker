using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using VoidWalker.Engine.Core.Data.Json.TileSet;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Core.Hosted;
using VoidWalker.Engine.Core.Interfaces.Services;
using VoidWalker.Engine.Core.ScriptModules;
using VoidWalker.Engine.Core.ScriptModules.Services;
using VoidWalker.Engine.Core.Utils;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Network.Extensions;
using VoidWalker.Engine.Network.Packets;
using VoidWalker.Engine.Server.Data.Configs;
using VoidWalker.Engine.Server.Handlers;
using VoidWalker.Engine.Server.Hosted;
using VoidWalker.Engine.Server.Hubs;
using VoidWalker.Engine.Server.Routes;
using VoidWalker.Engine.Server.Services;
using VoidWalker.Engine.Server.Utils;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);


var rootDirectory = DirectoriesUtils.GetRootDirectory();

Log.Logger.Information("Root directory: {rootDirectory}", rootDirectory);


builder.Services.AddLogging(opts => opts.ClearProviders().AddSerilog());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultJsonSettings();


builder.Services
    .RegisterScriptModule<LoggerModule>()
    .RegisterScriptModule<TileServiceModule>();


// builder.Services.AddAuthorization();
// builder.Services.AddAuthentication(
//         options =>
//         {
//             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         }
//     )
//     .AddJwtBearer(
//         options =>
//         {
//             options.Authority = "Authority URL";
//
//             options.Events = new JwtBearerEvents
//             {
//                 OnMessageReceived = context =>
//                 {
//                     var accessToken = context.Request.Query["access_token"];
//
//                     var path = context.HttpContext.Request.Path;
//                     if (!string.IsNullOrEmpty(accessToken) &&
//                         (path.StartsWithSegments("/hubs/game")))
//                     {
//                         context.Token = accessToken;
//                     }
//
//                     return Task.CompletedTask;
//                 }
//             };
//         }
//     );

builder.Services.AddCors();
builder.Services.AddSignalR();


builder.Services.RegisterRedis(builder.Configuration);


builder.Services
    .RegisterJsonMap<TileSetObject>();


builder.Services
    .RegisterVoidWalkerService<ISessionService, SessionService>()
    .RegisterVoidWalkerService<ITileSetService, TileSetService>()
    .RegisterVoidWalkerService<IMessageBusService, MessageBusService>()
    .RegisterVoidWalkerService<IScriptEngineService, ScriptEngineService>(true, 101)
    .RegisterVoidWalkerService<IDataLoaderService, DataLoaderService>(true, 100);

builder.Services.RegisterVoidWalkerService<OutputMessageEventHandler>();


builder.Services
    .AddHostedService<GameServerHostedService>()
    .AddHostedService<AutoStartHostedService>();


builder.Services.AddCors(
    options => options.AddPolicy(
        "Cors",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("https://gourav-d.github.io");
        }
    )
);


builder.Services.RegisterConfig<GameServiceConfig>(builder.Configuration, "Game");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseAuthentication()
//     .UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("Cors");


app.MapGet(
    "/test/message",
    async (IMessageBusService mediator) =>
    {
        var message = new SendOutputEvent(null, new HelloResponsePacket().ToNetworkPacketData(), true);

        mediator.Publish(message);

        return Results.Ok();
    }
);

app.MapTilesRoutes();

app.MapHub<GameHub>("hubs/game");


app.Run();
