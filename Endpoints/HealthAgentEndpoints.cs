using System.Security.Claims;
using MediAgent.Api.Common;
using MediAgent.Api.Models.Requests;
using MediAgent.Api.Services;

namespace MediAgent.Api.Endpoints;

public static class HealthAgentEndpoints
{
    public static void MapHealthAgentEndpoints(this WebApplication app)
    {
        var healthGroup = app.MapGroup("/api/health").RequireAuthorization();

        healthGroup.MapPost("/analyze", async (
            SymptomAnalysisRequest request,
            IHealthAgentService healthAgentService,
            HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            if (userId == null)
                return Results.Unauthorized();

            var result = await healthAgentService.AnalyzeSymptomsAsync(userId.Value, request);
            return Results.Ok(ApiResponse<object>.Ok(result, "Symptom analysis completed"));
        })
        .WithName("AnalyzeSymptoms")
        .WithOpenApi();

        healthGroup.MapPost("/chat", async (
            SymptomAnalysisRequest request,
            IHealthAgentService healthAgentService,
            HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            if (userId == null)
                return Results.Unauthorized();

            var result = await healthAgentService.AnalyzeSymptomsAsync(userId.Value, request);
            return Results.Ok(ApiResponse<object>.Ok(result, "Chat response generated"));
        })
        .WithName("ChatWithAgent")
        .WithOpenApi();

        healthGroup.MapGet("/suggestions", (string? q) =>
        {
            var suggestions = new[]
            {
                "Headache", "Fever", "Cough", "Sore throat", "Fatigue",
                "Nausea", "Dizziness", "Chest pain", "Shortness of breath",
                "Abdominal pain", "Back pain", "Joint pain", "Skin rash",
                "Allergic reaction", "Ear pain", "Eye irritation"
            };

            if (!string.IsNullOrWhiteSpace(q))
            {
                suggestions = suggestions
                    .Where(s => s.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
            }

            return Results.Ok(ApiResponse<object>.Ok(suggestions));
        })
        .WithName("GetSuggestions")
        .WithOpenApi();
    }

    private static Guid? GetUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdClaim, out var userId))
            return userId;
        return null;
    }
}
