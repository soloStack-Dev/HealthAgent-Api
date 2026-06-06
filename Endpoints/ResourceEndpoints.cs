using MediAgent.Api.Common;
using MediAgent.Api.Infrastructure.WebSearch;

namespace MediAgent.Api.Endpoints;

public static class ResourceEndpoints
{
    public static void MapResourceEndpoints(this WebApplication app)
    {
        var resourceGroup = app.MapGroup("/api/resources");

        resourceGroup.MapGet("/search", async (string? q, IWebSearchService webSearch) =>
        {
            if (string.IsNullOrWhiteSpace(q))
                return Results.BadRequest(ApiResponse<object>.Fail("Search query is required"));

            var results = await webSearch.SearchResourcesAsync(q);
            return Results.Ok(ApiResponse<object>.Ok(results, $"Found {results.Count} resources"));
        })
        .WithName("SearchResources")
        .WithOpenApi();

        resourceGroup.MapGet("/trending", () =>
        {
            var trending = new[]
            {
                new { Topic = "Seasonal Allergies", Query = "allergy symptoms treatment" },
                new { Topic = "Common Cold vs Flu", Query = "cold flu difference" },
                new { Topic = "Headache Relief", Query = "headache causes treatment" },
                new { Topic = "Digestive Health", Query = "digestive health tips" },
                new { Topic = "Stress Management", Query = "stress relief techniques" }
            };

            return Results.Ok(ApiResponse<object>.Ok(trending));
        })
        .WithName("GetTrendingResources")
        .WithOpenApi();
    }
}
