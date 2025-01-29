using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Commands.CreateRiskCategory;

public record CreateRiskCategory : IRequest<int>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class CreateRiskCategoryHandler(IApplicationDbContext context) : IRequestHandler<CreateRiskCategory, int>
{
    public async Task<int> Handle(CreateRiskCategory request, CancellationToken cancellationToken)
    {
        var entity = new RiskCategory()
        {
            Name = request.Name,
            Description = request.Description
        };
        
        context.RiskCategories.Add(entity);
        
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
