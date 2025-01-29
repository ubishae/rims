using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

namespace RIMS.Application.Tickets.Commands.CreateTicket;

public record CreateTicket : IRequest<int>
{
    public string? Title { get; init; }
    public string? Description { get; set; }
    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public int RiskId { get; set; }
}

public class CreateTicketHandler(IApplicationDbContext context) : IRequestHandler<CreateTicket, int>
{
    public async Task<int> Handle(CreateTicket request, CancellationToken cancellationToken)
    {
        var entity = new Ticket()
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = request.Status,
            DueDate = request.DueDate,
            RiskId = request.RiskId
        };
        
        context.Tickets.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}