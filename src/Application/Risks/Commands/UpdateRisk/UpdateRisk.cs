using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;
using RIMS.Domain.Enums;

namespace RIMS.Application.Risks.Commands.UpdateRisk;

public record UpdateRisk : IRequest
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal ImpactScore { get; init; }
    public decimal ProbabilityScore { get; init; }
    public decimal RiskScore { get; init; }
    public RiskStatus Status { get; init; }
    public int RiskCategoryId { get; init; }
}

public class UpdateRiskHandler(IApplicationDbContext context) : IRequestHandler<UpdateRisk>
{
    public async Task Handle(UpdateRisk request, CancellationToken cancellationToken)
    {
        var entity = await context.Risks.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Risk), request.Id.ToString());
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.ImpactScore = request.ImpactScore;
        entity.ProbabilityScore = request.ProbabilityScore;
        entity.RiskScore = request.RiskScore;
        entity.Status = request.Status;
        entity.RiskCategoryId = request.RiskCategoryId;

        await context.SaveChangesAsync(cancellationToken);
    }
}
