using OprawaObrazowWebApi.Resources;

namespace OprawaObrazowWebApi.Services.Interfaces;

public interface IUserService
{
    Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken);
    Task<string> Login(LoginResource resource, CancellationToken cancellationToken);
}