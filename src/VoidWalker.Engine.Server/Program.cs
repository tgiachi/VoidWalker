using MediatR;
using Serilog;
using VoidWalker.Engine.Core.Data.Json.TileSet;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Core.Hosted;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Network.Extensions;
using VoidWalker.Engine.Network.Packets;
using VoidWalker.Engine.Server.Data;
using VoidWalker.Engine.Server.Data.Configs;
using VoidWalker.Engine.Server.Hosted;
using VoidWalker.Engine.Server.Hubs;
using VoidWalker.Engine.Server.Interfaces;
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


builder.Services.AddCors();
builder.Services.AddSignalR();


builder.Services.RegisterRedis(builder.Configuration);
builder.Services.AddMediatR(
    opts =>
    {
        opts.RegisterServicesFromAssembly(typeof(SendOutputEvent).Assembly);
        opts.RegisterServicesFromAssembly(typeof(Program).Assembly);
    }
);


builder.Services
    .RegisterJsonMap<TileSetObject>();


builder.Services
    .RegisterVoidWalkerService<ISessionService, SessionService>()
    .RegisterVoidWalkerService<ITileSetService, TileSetService>()
    .RegisterVoidWalkerService<IScriptEngineService, ScriptEngineService>()
    .RegisterVoidWalkerService<IDataLoaderService, DataLoaderService>(true, 2000);


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


app.UseHttpsRedirection();

app.UseCors("Cors");


app.MapGet(
    "/test/message",
    async (IMediator mediator) =>
    {
        var message = new SendOutputEvent(null, new HelloResponsePacket().ToNetworkPacketData(), true);

        await mediator.Publish(message);

        return Results.Ok();
    }
);


app.MapHub<GameHub>("game");


app.Run();
