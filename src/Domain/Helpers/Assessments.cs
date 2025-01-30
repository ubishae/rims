using RIMS.Domain.Enums;
using RIMS.Domain.Models;

namespace RIMS.Domain.Helpers;

public static class Assessments
{
    /// <summary>
    /// Calculates a risk score based on probability and impact values
    /// </summary>
    /// <param name="probability">The probability score (0-10)</param>
    /// <param name="impact">The impact score (0-10)</param>
    /// <returns>A RiskAssessment containing the score and risk level</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when inputs are outside the valid range</exception>
    public static RiskAssessment CalculateRiskScore(decimal probability, decimal impact)
    {
        ValidateInput(probability, nameof(probability));
        ValidateInput(impact, nameof(impact));

        // Calculate the risk score (0-100)
        var riskScore = probability * impact;

        return new RiskAssessment
        {
            Score = riskScore,
            Level = DetermineRiskLevel(riskScore)
        };
    }

    /// <summary>
    /// Validates that the input value is within the acceptable range
    /// </summary>
    private static void ValidateInput(decimal value, string paramName)
    {
        if (value < 0 || value > 10)
        {
            throw new ArgumentOutOfRangeException(paramName, 
                $"{paramName} must be between 0 and 10");
        }
    }

    /// <summary>
    /// Determines the risk level based on the calculated score
    /// </summary>
    private static RiskLevel DetermineRiskLevel(decimal score)
    {
        return score switch
        {
            >= 80 => RiskLevel.Critical,
            >= 60 => RiskLevel.High,
            >= 40 => RiskLevel.Medium,
            >= 20 => RiskLevel.Low,
            _ => RiskLevel.Negligible
        };
    }
}