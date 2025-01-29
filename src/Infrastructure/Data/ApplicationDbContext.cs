using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Risk> Risks => Set<Risk>();
    public DbSet<RiskCategory> RiskCategories => Set<RiskCategory>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
}