using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using RIMS.Application.Common.Models;
using RIMS.Application.RiskCategories.Queries.GetRiskCategoriesWithPagination;
using RIMS.Application.Risks.Queries.GetRisksWithPagination;
using RIMS.Application.Tickets.Queries.GetTicketsWithPagination;

namespace RIMS.API.Endpoints;

/// <summary>
/// Endpoints for paginated queries.
/// </summary>
public class Paginated : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/v1/paginated")
            .WithTags("Paginated")
            //.RequireAuthorization()
            //.RequireRateLimiting("fixed");
            .WithOpenApi();
        
        group.MapGet("/tickets", GetTicketsWithPagination)
            .WithName("GetTicketsWithPagination")
            .WithDescription("Get paginated tickets")
            .Produces<PaginatedList<TicketPaginationDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        group.MapGet("/risks/categories", GetRiskCategoriesWithPagination)
            .WithName("GetRiskCategoriesWithPagination")
            .WithDescription("Get paginated risk categories")
            .Produces<PaginatedList<RiskCategoryPaginationDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        group.MapGet("/risks", GetRisksWithPagination)
            .WithName("GetRisksWithPagination")
            .WithDescription("Get paginated risks")
            .Produces<PaginatedList<RiskPaginationDto>>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }
    
    /// <summary>
    /// Get paginated tickets.
    /// </summary>
    private static async Task<Ok<PaginatedList<TicketPaginationDto>>> GetTicketsWithPagination(
        ISender sender, [AsParameters] GetTicketsWithPagination request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return TypedResults.Ok(await sender.Send(request));
    }

    /// <summary>
    /// Get paginated risk categories.
    /// </summary>
    private static async Task<Ok<PaginatedList<RiskCategoryPaginationDto>>> GetRiskCategoriesWithPagination(
        ISender sender, [AsParameters] GetRiskCategoriesWithPagination request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return TypedResults.Ok(await sender.Send(request));
    }

    /// <summary>
    /// Get paginated risks.
    /// </summary>
    private static async Task<Ok<PaginatedList<RiskPaginationDto>>> GetRisksWithPagination(
        ISender sender, [AsParameters] GetRisksWithPagination request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return TypedResults.Ok(await sender.Send(request));
    }
}