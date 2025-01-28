using RIMS.Domain.Entities;

namespace RIMS.Application.Tickets.Queries.GetTicketById;

public class TicketDetailsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Ticket, TicketDetailsDto>();
        }
    }
}