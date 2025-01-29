using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Application.Common.Mappings;
using RIMS.Application.Common.Models;

namespace RIMS.Application.RiskCategories.Queries.GetRiskCategoriesWithPagination;

public record GetRiskCategoriesWithPagination : IRequest<PaginatedList<RiskCategoryPaginationDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetRiskCategoriesWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetRiskCategoriesWithPagination, PaginatedList<RiskCategoryPaginationDto>>
{
    public async Task<PaginatedList<RiskCategoryPaginationDto>> Handle(GetRiskCategoriesWithPagination request, CancellationToken cancellationToken)
    {
        return await context.RiskCategories
            .OrderBy(x => x.Name)
            .ProjectTo<RiskCategoryPaginationDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
