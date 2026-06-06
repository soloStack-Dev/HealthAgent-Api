using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MediAgent.Api.Data;
using MediAgent.Api.Entities;
using MediAgent.Api.Models.Requests;

namespace MediAgent.Api.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ApplicationDbContext dbContext, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
        {
            return new AuthResult { Success = false, Error = "Email already registered" };
        }

        var user = new User
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12),
            FullName = request.FullName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
//store new register user data was go to database
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
//generate jwt token and refresh token because when new user was create the token was
//automatically some check to created and store to database
        var token = GenerateJwtToken(user);// expired in 15 minutes
        var refreshToken = GenerateRefreshToken();// expired in 7 days
        //when token was expired use referesh token to get new token

        //when registed our infromation to give request to server
        //the server was response the token and refresh token
        return new AuthResult
        {
            Success = true,
            Token = token,
            RefreshToken = refreshToken,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return new AuthResult { Success = false, Error = "Invalid email or password" };
        }

        var token = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        return new AuthResult
        {
            Success = true,
            Token = token,
            RefreshToken = refreshToken,
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email
        };
    }

    public Task<AuthResult> RefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException("Token refresh not yet implemented");
    }

    private string GenerateJwtToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "SuperSecretKeyForDevelopment123!"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//secret-key: SuperSecretKeyForDevelopment123!
//use algorithm: HmacSha256 for generate the token
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"] ?? "MediAgent",
            audience: jwtSettings["Audience"] ?? "MediAgentClient",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
