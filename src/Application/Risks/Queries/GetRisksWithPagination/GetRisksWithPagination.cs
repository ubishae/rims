using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Application.Common.Mappings;
using RIMS.Application.Common.Models;

namespace RIMS.Application.Risks.Queries.GetRisksWithPagination;

public record GetRisksWithPagination : IRequest<PaginatedList<RiskPaginationDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchString { get; init; }
    public int? CategoryId { get; init; }
}

public class GetRisksWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetRisksWithPagination, PaginatedList<RiskPaginationDto>>
{
    public async Task<PaginatedList<RiskPaginationDto>> Handle(GetRisksWithPagination request, CancellationToken cancellationToken)
    {
        var query = context.Risks
            .Include(x => x.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.SearchString))
        {
            query = query.Where(x => 
                x.Title!.Contains(request.SearchString) || 
                x.Description!.Contains(request.SearchString));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == request.CategoryId.Value);
        }

        return await query
            .OrderByDescending(x => x.RiskScore)
            .ProjectTo<RiskPaginationDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
