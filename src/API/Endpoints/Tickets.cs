using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using RIMS.Application.Tickets.Commands.CreateTicket;
using RIMS.Application.Tickets.Commands.DeleteTicket;
using RIMS.Application.Tickets.Commands.UpdateTicket;
using RIMS.Application.Tickets.Queries.GetTicketById;
using RIMS.Application.Tickets.Queries.GetTickets;
using RIMS.Domain.Entities;

namespace RIMS.API.Endpoints;

public class Tickets : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/tickets").WithTags("Tickets").WithOpenApi();
        
        group.MapGet("/", GetTickets).WithName("GetTickets");
        group.MapPost("", CreateTicket).WithName("CreateTicket");
        group.MapGet("/{id:int}", GetTicketById).WithName("GetTicketById");
        group.MapPut("/{id:int}", UpdateTicket).WithName("UpdateTicket");
        group.MapDelete("/{id:int}", DeleteTicket).WithName("DeleteTicket");
    }

    private static async Task<Ok<IEnumerable<TicketBriefDto>>> GetTickets(ISender sender)
    {
        return TypedResults.Ok(await sender.Send(new GetTickets()));
    }
    
    private static async Task<CreatedAtRoute> CreateTicket(ISender sender, CreateTicket request)
    {
        var id = await sender.Send(request);
        return TypedResults.CreatedAtRoute("GetTicketById", new { id });
    }
    
    private static async Task<Results<Ok<TicketDetailsDto>, BadRequest>> GetTicketById(ISender sender, int id)
    {
        return TypedResults.Ok(await sender.Send(new GetTicketById(id)));
    }
    
    private static async Task<Results<NoContent, BadRequest>> UpdateTicket(ISender sender, int id, UpdateTicket request)
    {
        if (id != request.Id) return TypedResults.BadRequest();
        
        await sender.Send(request);
        return TypedResults.NoContent();
    }
    
    private static async Task<Results<NoContent, BadRequest>> DeleteTicket(ISender sender, int id)
    {
        await sender.Send(new DeleteTicket(id));
        return TypedResults.NoContent();
    }
}