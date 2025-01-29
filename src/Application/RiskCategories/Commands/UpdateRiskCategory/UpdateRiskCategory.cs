using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Commands.UpdateRiskCategory;

public record UpdateRiskCategory : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class UpdateRiskCategoryHandler(IApplicationDbContext context) : IRequestHandler<UpdateRiskCategory>
{
    public async Task Handle(UpdateRiskCategory request, CancellationToken cancellationToken)
    {
        var entity = await context.RiskCategories.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RiskCategory), request.Id.ToString());
        }

        entity.Name = request.Name;
        entity.Description = request.Description;

        await context.SaveChangesAsync(cancellationToken);
    }
}
