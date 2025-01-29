using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Risks.Queries.GetRiskById;

public record GetRiskById(int Id) : IRequest<RiskDetailsDto>;

public class GetRiskByIdHandler(IApplicationDbContext context, IMapper mapper) 
    : IRequestHandler<GetRiskById, RiskDetailsDto>
{
    public async Task<RiskDetailsDto> Handle(GetRiskById request, CancellationToken cancellationToken)
    {
        var risk = await context.Risks
            .Include(r => r.RiskCategory)
            .ProjectTo<RiskDetailsDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (risk == null)
        {
            throw new NotFoundException(nameof(Risk), request.Id.ToString());
        }

        return risk;
    }
}
