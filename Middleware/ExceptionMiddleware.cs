using System.Net;
using System.Text.Json;
using MediAgent.Api.Common;

namespace MediAgent.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessage = context.RequestServices
                .GetService<IWebHostEnvironment>()?.IsDevelopment() == true
                    ? $"Server error: {ex.GetType().Name} - {ex.Message}"
                    : "An unexpected error occurred. Please try again later.";

            var response = ApiResponse<object>.Fail(errorMessage);
            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
