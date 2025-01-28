using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Queries.GetTickets;

public record GetTickets : IRequest<IEnumerable<TicketBriefDto>>;

public class GetTicketsHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetTickets, IEnumerable<TicketBriefDto>>
{
    public async Task<IEnumerable<TicketBriefDto>> Handle(GetTickets request, CancellationToken cancellationToken)
    {
        return await context.Tickets
            .ProjectTo<TicketBriefDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}