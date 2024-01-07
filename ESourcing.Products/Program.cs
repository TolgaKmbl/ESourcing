using ESourcing.Products.Configuration;
using ESourcing.Products.Data;
using ESourcing.Products.Data.Contract;
using ESourcing.Products.Repositories;
using ESourcing.Products.Repositories.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProductMongoDbSettings>(builder.Configuration.GetSection(nameof(ProductMongoDbSettings)));
builder.Services.AddSingleton<IProductMongoDbSettings>(sp => sp.GetRequiredService<IOptions<ProductMongoDbSettings>>().Value);

builder.Services.AddSingleton<IProductContext, ProductContext>();4
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

