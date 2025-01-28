using RIMS.Domain.Common;

namespace RIMS.Domain.Entities;

public class Ticket : BaseAuditableEntity
{
    public string? Title { get; set; }
}