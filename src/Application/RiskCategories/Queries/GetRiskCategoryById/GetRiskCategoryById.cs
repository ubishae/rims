using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.RiskCategories.Queries.GetRiskCategoryById;

public record GetRiskCategoryById(int Id) : IRequest<RiskCategoryDetailsDto>;

public class GetRiskCategoryByIdHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<GetRiskCategoryById, RiskCategoryDetailsDto>
{
    public async Task<RiskCategoryDetailsDto> Handle(GetRiskCategoryById request, CancellationToken cancellationToken)
    {
        var riskCategory = await context.RiskCategories
            .ProjectTo<RiskCategoryDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (riskCategory == null)
        {
            throw new NotFoundException(nameof(RiskCategory), request.Id.ToString());
        }

        return riskCategory;
    }
}
