using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.RateLimiting;
using RIMS.Application.Common.Models;
using RIMS.Application.Risks.Commands.CreateRisk;
using RIMS.Application.Risks.Commands.DeleteRisk;
using RIMS.Application.Risks.Commands.UpdateRisk;
using RIMS.Application.Risks.Queries.GetRiskById;
using RIMS.Application.Risks.Queries.GetRisks;
using RIMS.Domain.Entities;

namespace RIMS.API.Endpoints;

/// <summary>
/// Endpoints for managing risks.
/// </summary>
// [Authorize]
public class Risks : ICarterModule
{
    private const string BaseRoute = "api/v1/risks";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(BaseRoute)
            .WithTags("Risks")
            //.RequireAuthorization()
            //.RequireRateLimiting("fixed");
            .WithOpenApi();
        
        group.MapGet("/", GetRisks)
            .WithName("GetRisks")
            .WithDescription("Get all risks")
            .Produces<IEnumerable<RiskBriefDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapPost("", CreateRisk)
            .WithName("CreateRisk")
            .WithDescription("Create a new risk")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapGet("/{id:int}", GetRiskById)
            .WithName("GetRiskById")
            .WithDescription("Get a risk by ID")
            .Produces<RiskDetailsDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapPut("/{id:int}", UpdateRisk)
            .WithName("UpdateRisk")
            .WithDescription("Update an existing risk")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapDelete("/{id:int}", DeleteRisk)
            .WithName("DeleteRisk")
            .WithDescription("Delete a risk")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }

    /// <summary>
    /// Get all risks.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <returns>List of risks.</returns>
    private static async Task<Ok<IEnumerable<RiskBriefDto>>> GetRisks(ISender sender)
    {
        return TypedResults.Ok(await sender.Send(new GetRisks()));
    }
    
    /// <summary>
    /// Create a new risk.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="request">Create request.</param>
    /// <returns>Created risk ID.</returns>
    private static async Task<Results<Created<int>, ValidationProblem>> CreateRisk(
        ISender sender, CreateRisk request)
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
    /// Get a risk by ID.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Risk ID.</param>
    /// <returns>Risk details.</returns>
    private static async Task<Results<Ok<RiskDetailsDto>, NotFound>> GetRiskById(
        ISender sender, int id)
    {
        try
        {
            return TypedResults.Ok(await sender.Send(new GetRiskById(id)));
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
    
    /// <summary>
    /// Update an existing risk.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Risk ID.</param>
    /// <param name="request">Update request.</param>
    /// <returns>No content if successful.</returns>
    private static async Task<Results<NoContent, NotFound, ValidationProblem>> UpdateRisk(
        ISender sender, int id, UpdateRisk request)
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
    /// Delete a risk.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Risk ID.</param>
    /// <returns>No content if successful.</returns>
    private static async Task<Results<NoContent, NotFound>> DeleteRisk(ISender sender, int id)
    {
        try
        {
            await sender.Send(new DeleteRisk(id));
            return TypedResults.NoContent();
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
}
