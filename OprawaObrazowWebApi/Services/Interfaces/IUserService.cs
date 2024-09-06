using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Resources;

namespace OprawaObrazowWebApi.Services.Interfaces;

public interface IUserService
{
    Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
    Task<AccessTokens> Login(LoginResource resource, CancellationToken cancellationToken);
    Task<AccessTokens> RefreshTokens(string accessToken, string refreshToken, CancellationToken cancellationToken);
}