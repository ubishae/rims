using Microsoft.EntityFrameworkCore;
using RIMS.Domain.Entities;

namespace RIMS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Ticket> Tickets { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}