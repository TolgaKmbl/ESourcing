using ESourcing.Products.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.ConfigureContext();
builder.Services.ConfigureRepositories();

builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ESourcing.Sourcing",
            Version = "v1"
        }
        );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("../swagger/v1/swagger.json", "ESourcing.Sourcing v1"));
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
