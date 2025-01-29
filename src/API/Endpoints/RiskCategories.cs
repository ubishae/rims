using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.RateLimiting;
using RIMS.Application.Common.Models;
using RIMS.Application.RiskCategories.Commands.CreateRiskCategory;
using RIMS.Application.RiskCategories.Commands.DeleteRiskCategory;
using RIMS.Application.RiskCategories.Commands.UpdateRiskCategory;
using RIMS.Application.RiskCategories.Queries.GetRiskCategories;
using RIMS.Application.RiskCategories.Queries.GetRiskCategoryById;
using RIMS.Domain.Entities;

namespace RIMS.API.Endpoints;

/// <summary>
/// Endpoints for managing risk categories.
/// </summary>
// [Authorize]
public class RiskCategories : ICarterModule
{
    private const string BaseRoute = "api/v1/risks/categories";
    
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(BaseRoute)
            .WithTags("Risk Categories")
            //.RequireAuthorization()
            //.RequireRateLimiting("fixed");
            .WithOpenApi();
        
        group.MapGet("/", GetRiskCategories)
            .WithName("GetRiskCategories")
            .WithDescription("Get all risk categories")
            .Produces<IEnumerable<RiskCategoryBriefDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapPost("", CreateRiskCategory)
            .WithName("CreateRiskCategory")
            .WithDescription("Create a new risk category")
            .Produces<int>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapGet("/{id:int}", GetRiskCategoryById)
            .WithName("GetRiskCategoryById")
            .WithDescription("Get a risk category by ID")
            .Produces<RiskCategoryDetailsDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapPut("/{id:int}", UpdateRiskCategory)
            .WithName("UpdateRiskCategory")
            .WithDescription("Update an existing risk category")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
            
        group.MapDelete("/{id:int}", DeleteRiskCategory)
            .WithName("DeleteRiskCategory")
            .WithDescription("Delete a risk category")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }

    /// <summary>
    /// Get all risk categories.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <returns>List of risk categories.</returns>
    private static async Task<Ok<IEnumerable<RiskCategoryBriefDto>>> GetRiskCategories(ISender sender)
    {
        return TypedResults.Ok(await sender.Send(new GetRiskCategories()));
    }
    
    /// <summary>
    /// Create a new risk category.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="request">Create request.</param>
    /// <returns>Created risk category ID.</returns>
    private static async Task<Results<Created<int>, ValidationProblem>> CreateRiskCategory(
        ISender sender, CreateRiskCategory request)
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
    /// Get a risk category by ID.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Risk category ID.</param>
    /// <returns>Risk category details.</returns>
    private static async Task<Results<Ok<RiskCategoryDetailsDto>, NotFound>> GetRiskCategoryById(
        ISender sender, int id)
    {
        try
        {
            return TypedResults.Ok(await sender.Send(new GetRiskCategoryById(id)));
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
    
    /// <summary>
    /// Update an existing risk category.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Risk category ID.</param>
    /// <param name="request">Update request.</param>
    /// <returns>No content if successful.</returns>
    private static async Task<Results<NoContent, NotFound, ValidationProblem>> UpdateRiskCategory(
        ISender sender, int id, UpdateRiskCategory request)
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
    /// Delete a risk category.
    /// </summary>
    /// <param name="sender">MediatR sender.</param>
    /// <param name="id">Risk category ID.</param>
    /// <returns>No content if successful.</returns>
    private static async Task<Results<NoContent, NotFound>> DeleteRiskCategory(ISender sender, int id)
    {
        try
        {
            await sender.Send(new DeleteRiskCategory(id));
            return TypedResults.NoContent();
        }
        catch (NotFoundException)
        {
            return TypedResults.NotFound();
        }
    }
}
