using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;

namespace RIMS.Application.RiskCategories.Queries.GetRiskCategories;

public record GetRiskCategories : IRequest<IEnumerable<RiskCategoryBriefDto>>;

public class GetRiskCategoriesHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<GetRiskCategories, IEnumerable<RiskCategoryBriefDto>>
{
    public async Task<IEnumerable<RiskCategoryBriefDto>> Handle(GetRiskCategories request, CancellationToken cancellationToken)
    {
        return await context.RiskCategories
            .ProjectTo<RiskCategoryBriefDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
