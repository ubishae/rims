using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

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
        if (!context.RiskCategories.Any() && !context.Risks.Any() && !context.Tickets.Any())
        {
            context.Risks.Add(new Risk()
            {
                Title = "Market Volatility",
                Description = "High market fluctuations affecting revenue",
                ImpactScore = 8,
                ProbabilityScore = 7,
                RiskScore = 56,
                Status = RiskStatus.Active,
                RiskCategory = new RiskCategory()
                {
                    Name = "Financial Risk",
                    Description = "Risks related to financial instability"
                },
                Tickets = new List<Ticket>()
                {
                    new() { Title = "Monitor market trends", Description = "Gather financial data trends", Priority = TicketPriority.High, Status = TicketStatus.InProgress, DueDate = new DateTime(2024, 02, 01) },
                },
            });
            
            context.Risks.Add(new Risk()
            {
                Title = "GDPR Non-Compliance",
                Description = "Potential data privacy violations",
                ImpactScore = 9,
                ProbabilityScore = 6,
                RiskScore = 54,
                Status = RiskStatus.Identified,
                RiskCategory = new RiskCategory()
                {
                    Name = "Compliance Risk",
                    Description = "Risks related to regulatory compliance"
                },
                Tickets = new List<Ticket>()
                {
                    new() { Title = "Review GDPR policy", Description = "Assess compliance framework", Priority = TicketPriority.Medium, Status = TicketStatus.Pending, DueDate = new DateTime(2024, 01, 19) },
                },
            });
            
            context.Risks.Add(new Risk()
            {
                Title = "System Downtime",
                Description = "Frequent application crashes",
                ImpactScore = 7,
                ProbabilityScore = 8,
                RiskScore = 56,
                Status = RiskStatus.Active,
                RiskCategory = new RiskCategory()
                {
                    Name = "Operational Risk",
                    Description = "Risks arising from business operations"
                },
                Tickets = new List<Ticket>()
                {
                    new() { Title = "Fix downtime issues", Description = "Investigate system failures", Priority = TicketPriority.High, Status = TicketStatus.Completed, DueDate = new DateTime(2024, 01, 22) },
                },
            });

            await context.SaveChangesAsync();
        }
    }
}