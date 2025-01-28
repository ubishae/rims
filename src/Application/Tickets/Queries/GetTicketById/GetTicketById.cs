using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Queries.GetTicketById;

public record GetTicketById(int Id) : IRequest<TicketDetailsDto>;

public class GetTicketByIdHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetTicketById, TicketDetailsDto>
{
    public async Task<TicketDetailsDto> Handle(GetTicketById request, CancellationToken cancellationToken)
    {
        var entity = await context.Tickets
            .ProjectTo<TicketDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new Exception($"Ticket {request.Id} not found.");
        }
        
        return entity;
    }
}