using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Enums;

namespace RIMS.Application.Tickets.Commands.UpdateTicket;

public record UpdateTicket : IRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public int RiskId { get; set; }
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
        entity.Description = request.Description;
        entity.Priority = request.Priority;
        entity.Status = request.Status;
        entity.DueDate = request.DueDate;
        entity.RiskId = request.RiskId;
        
        await context.SaveChangesAsync(cancellationToken);
    }
}