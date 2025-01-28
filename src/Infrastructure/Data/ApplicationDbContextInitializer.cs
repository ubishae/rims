using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RIMS.Domain.Entities;

namespace RIMS.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!context.Tickets.Any())
        {
            await context.Tickets.AddRangeAsync(new []
            {
                new Ticket { Title = Faker.Lorem.Sentence() },
                new Ticket { Title = Faker.Lorem.Sentence() },
                new Ticket { Title = Faker.Lorem.Sentence() },
            });

            await context.SaveChangesAsync();
        }
    }
}