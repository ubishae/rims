using RIMS.Domain.Enums;

namespace RIMS.Domain.Models;

/// <summary>
/// Represents the result of a risk calculation
/// </summary>
public class RiskAssessment
{
    public decimal Score { get; set; }
    public RiskLevel Level { get; set; }
}