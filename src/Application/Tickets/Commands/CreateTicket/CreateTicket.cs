using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Commands.CreateTicket;

public record CreateTicket : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTicketHandler(IApplicationDbContext context) : IRequestHandler<CreateTicket, int>
{
    public Task<int> Handle(CreateTicket request, CancellationToken cancellationToken)
    {
        var entity = new Ticket()
        {
            Title = request.Title
        };
        
        context.Tickets.Add(entity);
        
        return context.SaveChangesAsync(cancellationToken);
    }
}