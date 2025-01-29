using Microsoft.EntityFrameworkCore;
using RIMS.Domain.Entities;

namespace RIMS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Risk> Risks { get; }
    DbSet<RiskCategory> RiskCategories { get; }
    DbSet<Ticket> Tickets { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}