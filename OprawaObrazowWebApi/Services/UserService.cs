using System.IdentityModel.Tokens.Jwt;
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

    public async Task<string> Login(LoginResource resource, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsername(resource.Username, cancellationToken);

        if (user == null) throw new Exception("Username or password did not match.");

        var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
        if (user.PasswordHash != passwordHash) throw new Exception("Username or password did not match.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var secToken = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Issuer"], null,
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(secToken);

        return token;
    }
}