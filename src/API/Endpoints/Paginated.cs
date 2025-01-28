using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using RIMS.Application.Common.Models;
using RIMS.Application.Tickets.Queries.GetTicketsWithPagination;

namespace RIMS.API.Endpoints;

public class Paginated : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/paginated").WithTags("Paginated").WithOpenApi();
        
        group.MapGet("/tickets", GetTicketsWithPagination).WithName("GetTicketsWithPagination");
    }
    
    public static async Task<Ok<PaginatedList<TicketPaginationDto>>> GetTicketsWithPagination(ISender sender)
    {
        return TypedResults.Ok(await sender.Send(new GetTicketsWithPagination()));
    }
}