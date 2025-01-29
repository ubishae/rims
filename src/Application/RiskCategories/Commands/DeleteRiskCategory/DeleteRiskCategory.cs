using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Commands.DeleteRiskCategory;

public record DeleteRiskCategory(int Id) : IRequest;

public class DeleteRiskCategoryHandler(IApplicationDbContext context) : IRequestHandler<DeleteRiskCategory>
{
    public async Task Handle(DeleteRiskCategory request, CancellationToken cancellationToken)
    {
        var entity = await context.RiskCategories.FirstOrDefaultAsync(rc => rc.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(RiskCategory), request.Id.ToString());
        }

        context.RiskCategories.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
