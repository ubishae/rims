using RIMS.Application.Common.Interfaces;
using RIMS.Application.Common.Mappings;
using RIMS.Application.Common.Models;

namespace RIMS.Application.Tickets.Queries.GetTicketsWithPagination;

public class GetTicketsWithPagination : IRequest<PaginatedList<TicketPaginationDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTicketsWithPaginationHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetTicketsWithPagination, PaginatedList<TicketPaginationDto>>
{
    public async Task<PaginatedList<TicketPaginationDto>> Handle(GetTicketsWithPagination request, CancellationToken cancellationToken)
    {
        return await context.Tickets
            .OrderBy(x => x.Title)
            .ProjectTo<TicketPaginationDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}