using RIMS.Domain.Common;
using RIMS.Domain.Enums;

namespace RIMS.Domain.Entities;

public class Risk : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? ImpactScore { get; set; }
    public decimal? ProbabilityScore { get; set; }
    public decimal? RiskScore { get; set; }
    public RiskStatus? Status { get; set; }
    public RiskLevel? Level { get; set; }
    
    public int? CategoryId { get; set; }
    public RiskCategory? Category { get; set; }
    
    public List<Ticket>? Tickets { get; set; }
}