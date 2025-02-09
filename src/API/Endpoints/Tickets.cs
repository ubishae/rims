using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.RateLimiting;
using RIMS.Application.Common.Models;
using RIMS.Application.Tickets.Commands.CreateTicket;
using RIMS.Application.Tickets.Commands.DeleteTicket;
using RIMS.Application.Tickets.Commands.UpdateTicket;
using RIMS.Application.Tickets.Queries.GetTicketById;
using RIMS.Application.Tickets.Queries.GetTickets;
using RIMS.Domain.Entities;

namespace RIMS.API.Endpoints;

/// <summary>
/// Endpoints for managing tickets.
/// </summary>
// [Authorize]
public class Tickets : ICarterModule
{
    private const string BaseRoute = "api/v1/tickets";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(BaseRoute)
            .WithTags("Tickets")
            //.RequireAuthorization()
            //.RequireRateLimiting("fixed");
            .WithOpenApi();
        
        group.MapGet("/", GetTickets)
            .WithName("GetTickets")
            .WithDescription("Get all tickets")
            .Produces<IEnumerable<TicketBriefDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapPost("", CreateTicket)
            .WithName("CreateTicket")
            .WithDescription("Create a new ticket")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapGet("/{id:int}", GetTicketById)
            .WithName("GetTicketById")
            .WithDescription("Get a ticket by ID")
            .Produces<TicketDetailsDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapPut("/{id:int}", UpdateTicket)
            .WithName("UpdateTicket")
            .WithDescription("Update an existing ticket")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapDelete("/{id:int}", DeleteTicket)
            .WithName("DeleteTicket")
            .WithDescription("Delete a ticket")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }

    /// <summary>
    /// Get all tickets.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <returns>List of tickets.</returns>
    private static async Task<Ok<IEnumerable<TicketBriefDto>>> GetTickets(ISender sender)
    {
        return TypedResults.Ok(await sender.Send(new GetTickets()));
    }
    
    /// <summary>
    /// Create a new ticket.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="request">Create request.</param>
    /// <returns>Created ticket ID.</returns>
    private static async Task<Results<Created<int>, ValidationProblem>> CreateTicket(
        ISender sender, CreateTicket request)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        try
        {
            var id = await sender.Send(request);
            return TypedResults.Created($"/{BaseRoute}/{id}", id);
        }
        catch (ValidationException ex)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "validationErrors", [ex.Message] }
            };
            
            return TypedResults.ValidationProblem(errors);
        }
    }
    
    /// <summary>
    /// Get a ticket by ID.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Ticket ID.</param>
    /// <returns>Ticket details.</returns>
    private static async Task<Results<Ok<TicketDetailsDto>, NotFound>> GetTicketById(
        ISender sender, int id)
    {
        try
        {
            return TypedResults.Ok(await sender.Send(new GetTicketById(id)));
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
    
    /// <summary>
    /// Update an existing ticket.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Ticket ID.</param>
    /// <param name="request">Update request.</param>
    /// <returns>No content if successful.</returns>
    private static async Task<Results<NoContent, NotFound, ValidationProblem>> UpdateTicket(
        ISender sender, int id, UpdateTicket request)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        if (id != request.Id) 
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                { "Id", new[] { "ID in route must match ID in request body" } }
            });
        }
        
        try
        {
            await sender.Send(request);
            return TypedResults.NoContent();
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
        catch (ValidationException ex)
        {
            var errors = new Dictionary<string, string[]>
            {
                { "validationErrors", [ex.Message] }
            };
            
            return TypedResults.ValidationProblem(errors);
        }
    }
    
    /// <summary>
    /// Delete a ticket.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Ticket ID.</param>
    /// <returns>No content if successful.</returns>
    private static async Task<Results<NoContent, NotFound>> DeleteTicket(ISender sender, int id)
    {
        try
        {
            await sender.Send(new DeleteTicket(id));
            return TypedResults.NoContent();
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
}