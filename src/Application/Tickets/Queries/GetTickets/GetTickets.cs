using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Queries.GetTickets;

public record GetTickets : IRequest<IEnumerable<Ticket>>;

public class GetTicketsHandler(IApplicationDbContext context) : IRequestHandler<GetTickets, IEnumerable<Ticket>>
{
    public async Task<IEnumerable<Ticket>> Handle(GetTickets request, CancellationToken cancellationToken)
    {
        return await context.Tickets.ToListAsync(cancellationToken);
    }
}