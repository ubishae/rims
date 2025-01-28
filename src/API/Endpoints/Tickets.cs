using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using RIMS.Application.Tickets.Queries.GetTickets;
using RIMS.Domain.Entities;

namespace RIMS.API.Endpoints;

public class Tickets : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/tickets").WithTags("Tickets").WithOpenApi();
        
        group.MapGet("/", GetTickets).WithName("GetTickets");
    }

    private static async Task<Ok<IEnumerable<Ticket>>> GetTickets(ISender sender)
    {
        return TypedResults.Ok(await sender.Send(new GetTickets()));
    }
}