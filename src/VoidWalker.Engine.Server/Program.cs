using Serilog;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Core.Hosted;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Server.Data;
using VoidWalker.Engine.Server.Data.Configs;
using VoidWalker.Engine.Server.Hosted;
using VoidWalker.Engine.Server.Hubs;
using VoidWalker.Engine.Server.Interfaces;
using VoidWalker.Engine.Server.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

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


builder.Services.RegisterVoidWalkerService<ISessionService, SessionService>();
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


app.MapHub<GameHub>("game");


app.Run();
