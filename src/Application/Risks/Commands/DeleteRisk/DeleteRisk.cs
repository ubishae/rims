using MediatR;
using RIMS.Application.Common.Interfaces;
using RIMS.Domain.Entities;

namespace RIMS.Application.Risks.Commands.DeleteRisk;

public record DeleteRisk(int Id) : IRequest;

public class DeleteRiskHandler(IApplicationDbContext context) : IRequestHandler<DeleteRisk>
{
    public async Task Handle(DeleteRisk request, CancellationToken cancellationToken)
    {
        var entity = await context.Risks.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Risk), request.Id.ToString());
        }

        context.Risks.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
