using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Queries.GetTickets;

public class TicketBriefDto
{
    public string? Title { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Ticket, TicketBriefDto>();
        }
    }
}