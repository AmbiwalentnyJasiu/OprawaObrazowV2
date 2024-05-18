using OprawaObrazowWebApi.Models;

namespace OprawaObrazowWebApi.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddUser(User user, CancellationToken cancellationToken);
    Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
}