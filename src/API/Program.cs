using Carter;
using RIMS.Infrastructure;
using RIMS.Infrastructure.Data;
using Scalar.AspNetCore;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

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
app.UseCors(myAllowSpecificOrigins);

app.Run();