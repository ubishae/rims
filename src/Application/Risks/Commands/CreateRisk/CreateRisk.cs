using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;
using RIMS.Domain.Helpers;

namespace RIMS.Application.Risks.Commands.CreateRisk;

public record CreateRisk : IRequest<int>
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal ImpactScore { get; init; }
    public decimal ProbabilityScore { get; init; }
    public int CategoryId { get; init; }
}

public class CreateRiskHandler(IApplicationDbContext context) : IRequestHandler<CreateRisk, int>
{
    public async Task<int> Handle(CreateRisk request, CancellationToken cancellationToken)
    {
        var riskAssessment = Assessments.CalculateRiskScore(request.ProbabilityScore, request.ImpactScore);
        
        var entity = new Risk()
        {
            Title = request.Title,
            Description = request.Description,
            ImpactScore = request.ImpactScore,
            ProbabilityScore = request.ProbabilityScore,
            RiskScore = riskAssessment.Score,
            Level = riskAssessment.Level,
            Status = RiskStatus.Active,
            CategoryId = request.CategoryId
        };
        
        context.Risks.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
