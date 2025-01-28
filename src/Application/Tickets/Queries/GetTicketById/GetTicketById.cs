using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Queries.GetTicketById;

public record GetTicketById(int Id) : IRequest<Ticket>;

public class GetTicketByIdHandler(IApplicationDbContext context) : IRequestHandler<GetTicketById, Ticket>
{
    public async Task<Ticket> Handle(GetTicketById request, CancellationToken cancellationToken)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new Exception($"Ticket {request.Id} not found.");
        }
        
        return entity;
    }
}