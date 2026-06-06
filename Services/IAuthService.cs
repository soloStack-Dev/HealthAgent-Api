using MediAgent.Api.Models.Requests;

namespace MediAgent.Api.Services;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> LoginAsync(LoginRequest request);
    Task<AuthResult> RefreshTokenAsync(string refreshToken);
}

public class AuthResult
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public string? Error { get; set; }
    public Guid? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}

/*
store the user info with authentication details in database and store referesh token in database
normal token was expire in 15 minutes after 15 min we need to generate new token
request to server old token with before we recieved referesh token to server 
the server was check the referesh token is valid
if valid, return new token
if not valid, return error
*/