using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using VoidWalker.AuthService.Context;
using VoidWalker.AuthService.Dao;
using VoidWalker.AuthService.Interfaces;
using VoidWalker.AuthService.Seeds;
using VoidWalker.AuthService.Service;
using VoidWalker.Engine.Core.Data;
using VoidWalker.Engine.Core.Extensions;
using VoidWalker.Engine.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder => loggingBuilder.ClearProviders().AddSerilog());

builder.Services.AddTransient<ILoginService, LoginService>();


builder.Services.RegisterConfig<JwtConfigData>(builder.Configuration, "Jwt");


builder.Services.AddDbContextFactory<AuthServiceDbContext>(
    options =>
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));


        options.UseNpgsql(dataSourceBuilder.Build())
            .UseCamelCaseNamingConvention();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet(
        "/weatherforecast",
        () =>
        {
            var forecast = Enumerable.Range(1, 5)
                .Select(
                    index =>
                        new WeatherForecast(
                            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            Random.Shared.Next(-20, 55),
                            summaries[Random.Shared.Next(summaries.Length)]
                        )
                )
                .ToArray();
            return forecast;
        }
    )
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
