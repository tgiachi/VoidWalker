using ConfigurationSubstitution;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using VoidWalker.AuthService.Context;
using VoidWalker.AuthService.Dao;
using VoidWalker.AuthService.Hubs;
using VoidWalker.AuthService.Interfaces;
using VoidWalker.AuthService.Routes;
using VoidWalker.AuthService.Seeds;
using VoidWalker.AuthService.Service;
using VoidWalker.Engine.Core.Data;
using VoidWalker.Engine.Core.Data.Auth;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Core.Hosted;
using VoidWalker.Engine.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();


builder.Configuration
    .AddEnvironmentVariables()
    .EnableSubstitutions();


builder.Services.AddLogging(loggingBuilder => loggingBuilder.ClearProviders().AddSerilog());

builder.Services.AddTransient<ILoginService, LoginService>();


builder.Services.RegisterRedis(builder.Configuration);
builder.Services.AddHostedService<AutoStartHostedService>();
builder.Services.RegisterVoidWalkerService<IShardService, ShardService>();

builder.Services.RegisterConfig<JwtConfigData>(builder.Configuration, "Jwt");


builder.Services.AddDbContextFactory<AuthServiceDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
            .UseSnakeCaseNamingConvention();
    }
);

builder.Services.RegisterDataAccess(typeof(AuthServiceDataAccess<>));
builder.Services.AddDbMigrationService<AuthServiceDbContext>();

builder.Services.AddDbSeedService();
builder.Services.RegisterDbSeed<RoleAndUserSeed>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();


builder.Services.AddCors(
    options => options.AddPolicy(
        "Cors",
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:3000");
        }
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Cors");

app.MapHub<LoginHub>("login");

var apiGroup = app.MapGroup("/api/v1");

apiGroup
    .MapVersionRoute()
    .MapLoginRoute();

app.Run();
