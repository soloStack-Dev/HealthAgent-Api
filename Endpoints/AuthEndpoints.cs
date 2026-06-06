using MediAgent.Api.Common;
using MediAgent.Api.Models.Requests;
using MediAgent.Api.Services;

namespace MediAgent.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/api/auth");

        authGroup.MapPost("/register", async (RegisterRequest request, IAuthService authService) =>
        {
            var result = await authService.RegisterAsync(request);
            if (!result.Success)
                return Results.BadRequest(ApiResponse<object>.Fail(result.Error!));

            return Results.Ok(ApiResponse<object>.Ok(new
            {
                result.Token,
                result.RefreshToken,
                result.UserId,
                result.FullName,
                result.Email
            }, "Registration successful"));
        })
        .WithName("Register")
        .WithOpenApi();

        authGroup.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
        {
            var result = await authService.LoginAsync(request);
            if (!result.Success)
                return Results.BadRequest(ApiResponse<object>.Fail(result.Error!));

            return Results.Ok(ApiResponse<object>.Ok(new
            {
                result.Token,
                result.RefreshToken,
                result.UserId,
                result.FullName,
                result.Email
            }, "Login successful"));
        })
        .WithName("Login")
        .WithOpenApi();

        authGroup.MapPost("/refresh", async (string refreshToken, IAuthService authService) =>
        {
            var result = await authService.RefreshTokenAsync(refreshToken);
            if (!result.Success)
                return Results.Unauthorized();

            return Results.Ok(ApiResponse<object>.Ok(new
            {
                result.Token,
                result.RefreshToken
            }, "Token refreshed"));
        })
        .WithName("RefreshToken")
        .WithOpenApi();
    }
}
