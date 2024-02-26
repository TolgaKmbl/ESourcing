using Microsoft.AspNetCore.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOcelot();

builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile("ocelot.json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.UseOcelot();

app.Run();
