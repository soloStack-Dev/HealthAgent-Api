using System.Security.Claims;
using MediAgent.Api.Common;
using MediAgent.Api.Services;

namespace MediAgent.Api.Endpoints;

public static class ConversationEndpoints
{
    public static void MapConversationEndpoints(this WebApplication app)
    {
        var conversationGroup = app.MapGroup("/api/conversations").RequireAuthorization();

        conversationGroup.MapGet("/", async (IConversationService conversationService, HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            if (userId == null)
                return Results.Unauthorized();

            var conversations = await conversationService.GetUserConversationsAsync(userId.Value);
            return Results.Ok(ApiResponse<object>.Ok(conversations));
        })
        .WithName("ListConversations")
        .WithOpenApi();

        conversationGroup.MapGet("/{id:guid}", async (Guid id, IConversationService conversationService, HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            if (userId == null)
                return Results.Unauthorized();

            var conversation = await conversationService.GetConversationAsync(id, userId.Value);
            if (conversation == null)
                return Results.NotFound(ApiResponse<object>.Fail("Conversation not found"));

            return Results.Ok(ApiResponse<object>.Ok(conversation));
        })
        .WithName("GetConversation")
        .WithOpenApi();

        conversationGroup.MapDelete("/{id:guid}", async (Guid id, IConversationService conversationService, HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            if (userId == null)
                return Results.Unauthorized();

            var deleted = await conversationService.DeleteConversationAsync(id, userId.Value);
            if (!deleted)
                return Results.NotFound(ApiResponse<object>.Fail("Conversation not found"));

            return Results.Ok(ApiResponse<object>.Ok(null, "Conversation deleted"));
        })
        .WithName("DeleteConversation")
        .WithOpenApi();

        conversationGroup.MapPost("/{id:guid}/title", async (Guid id, IConversationService conversationService, HttpContext httpContext) =>
        {
            var userId = GetUserId(httpContext);
            if (userId == null)
                return Results.Unauthorized();

            var title = await conversationService.GenerateTitleAsync(id, userId.Value);
            if (title == null)
                return Results.NotFound(ApiResponse<object>.Fail("Conversation not found"));

            return Results.Ok(ApiResponse<object>.Ok(new { title }, "Title generated"));
        })
        .WithName("GenerateTitle")
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
