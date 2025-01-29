using RIMS.Domain.Common;

namespace RIMS.Domain.Entities;

public class RiskCategory : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public List<Risk>? Risks { get; set; }
}