using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;

namespace RIMS.Application.Tickets.Commands.UpdateTicket;

public record UpdateTicket : IRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
}

public class UpdateTicketHandler(IApplicationDbContext context) : IRequestHandler<UpdateTicket>
{
    public async Task Handle(UpdateTicket request, CancellationToken cancellationToken)
    {
        var entity = await context.Tickets.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (entity == null)
        {
            throw new Exception($"Ticket {request.Id} not found.");
        }
        
        entity.Title = request.Title;
        
        await context.SaveChangesAsync(cancellationToken);
    }
}