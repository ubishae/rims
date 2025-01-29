using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

namespace RIMS.Application.Risks.Commands.CreateRisk;

public record CreateRisk : IRequest<int>
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal ImpactScore { get; init; }
    public decimal ProbabilityScore { get; init; }
    public decimal RiskScore { get; init; }
    public RiskStatus Status { get; init; }
    public int RiskCategoryId { get; init; }
}

public class CreateRiskHandler(IApplicationDbContext context) : IRequestHandler<CreateRisk, int>
{
    public async Task<int> Handle(CreateRisk request, CancellationToken cancellationToken)
    {
        var entity = new Risk()
        {
            Title = request.Title,
            Description = request.Description,
            ImpactScore = request.ImpactScore,
            ProbabilityScore = request.ProbabilityScore,
            RiskScore = request.RiskScore,
            Status = request.Status,
            RiskCategoryId = request.RiskCategoryId
        };
        
        context.Risks.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
