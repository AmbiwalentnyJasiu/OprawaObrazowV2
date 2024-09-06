using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OprawaObrazowWebApi.Helpers;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;
using OprawaObrazowWebApi.Resources;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Services;

public class UserService(IUserRepository userRepository, IConfiguration configuration) : IUserService
{
    private readonly string _pepper = configuration["Pepper"] ?? throw new KeyNotFoundException("Pepper missing in configuration");
    private readonly int _iteration = Convert.ToInt32(configuration["IterationCount"] ?? throw new KeyNotFoundException("IterationCount missing in configuration"));

    public async Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Username = resource.Username,
            PasswordSalt = PasswordHasher.GenerateSalt()
        };
        user.PasswordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);

        await userRepository.AddUser(user, cancellationToken);

        return new UserResource(user.Id, user.Username);
    }

    public async Task<AccessTokens> Login(LoginResource resource, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsername(resource.Username, cancellationToken);

        if (user == null) throw new Exception("Username or password did not match.");

        var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
        if (user.PasswordHash != passwordHash) throw new Exception("Username or password did not match.");

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, resource.Username)
        };
        
        var secToken = CreateToken(authClaims);

        var token = new JwtSecurityTokenHandler().WriteToken(secToken);

        var result = new AccessTokens
        {
            AccessToken = token,
            RefreshToken = GenerateRefreshToken(),
            Expiration = secToken.ValidTo
        };

        return result;
    }

    public async Task<AccessTokens> RefreshTokens(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        var principal = GetPrincipalFromExpiredToken(accessToken);
        if (principal?.Identity?.Name == null)
        {
            throw new ArgumentException("Invalid access token or refresh token");
        }
        
        var username = principal.Identity.Name;
        
        var user = await userRepository.GetByUsername(username, cancellationToken);

        if (user == null)
        {
            throw new ArgumentException("Invalid access token or refresh token");
        }
        
        var newAccessToken = CreateToken(principal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();
        
        var result = new AccessTokens
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
            Expiration = newAccessToken.ValidTo
        };
        
        return result;
    }


    private JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Issuer"], authClaims,
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }
}