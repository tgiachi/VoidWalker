using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Network.Events;
using VoidWalker.Engine.Server.Data;
using VoidWalker.Engine.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultJsonSettings();

builder.Services.AddSignalR();

builder.Services.AddMediatR(
    opts =>
    {
        opts.RegisterServicesFromAssembly(typeof(SendOutputEvent).Assembly);
        opts.RegisterServicesFromAssembly(typeof(Program).Assembly);
    }
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

app.MapHub<GameHub>("game");

app.Run();
