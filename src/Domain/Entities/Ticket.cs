using RIMS.Domain.Common;
using RIMS.Domain.Enums;

namespace RIMS.Domain.Entities;

public class Ticket : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TicketPriority? Priority { get; set; }
    public TicketStatus? Status { get; set; }
    public DateTime? DueDate { get; set; }
    
    public int? RiskId { get; set; }
    public Risk? Risk { get; set; }
}