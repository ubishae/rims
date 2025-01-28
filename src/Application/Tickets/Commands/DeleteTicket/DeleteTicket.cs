using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;

namespace RIMS.Application.Tickets.Commands.DeleteTicket;

public record DeleteTicket(int Id) : IRequest;

public class DeleteTicketHandler(IApplicationDbContext context) : IRequestHandler<DeleteTicket>
{
    public async Task Handle(DeleteTicket request, CancellationToken cancellationToken)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new Exception($"Ticket {request.Id} not found.");
        }
        
        context.Tickets.Remove(entity);
        
        await context.SaveChangesAsync(cancellationToken);
    }
}