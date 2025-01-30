using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;

namespace RIMS.Application.Risks.Queries.GetRisks;

public record GetRisks : IRequest<IEnumerable<RiskBriefDto>>;

public class GetRisksHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<GetRisks, IEnumerable<RiskBriefDto>>
{
    public async Task<IEnumerable<RiskBriefDto>> Handle(GetRisks request, CancellationToken cancellationToken)
    {
        return await context.Risks
            .Include(r => r.Category)
            .ProjectTo<RiskBriefDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
