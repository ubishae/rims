using Carter;
using RIMS.Infrastructure;
using RIMS.Infrastructure.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.AddApplicationServices();
builder.AddInfrastructureServices();

builder.Services.AddCarter();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    await app.InitializeDatabaseAsync();
}

app.MapCarter();
app.UseHttpsRedirection();

app.Run();