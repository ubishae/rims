using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RIMS.Application.Common.Interfaces;
using RIMS.Infrastructure.Data;

namespace RIMS.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddSqlServerDbContext<ApplicationDbContext>("sql");
        
        builder.EnrichSqlServerDbContext<ApplicationDbContext>();
        
        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddScoped<ApplicationDbContextInitializer>();
    }
}
