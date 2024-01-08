using ESourcing.Products.Configuration;
using ESourcing.Products.Configuration.MongoDb;
using ESourcing.Products.Data;
using ESourcing.Products.Data.Contract;
using ESourcing.Products.Repositories;
using ESourcing.Products.Repositories.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ESourcing.Products",
        Version = "v1"
    });
});

builder.Services.AddControllers();
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "ESourcing.Products v1"));
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

