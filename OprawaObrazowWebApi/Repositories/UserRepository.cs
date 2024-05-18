using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class UserRepository(DatabaseContext context) : IUserRepository
{
    public async Task AddUser(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByUsername(string username, CancellationToken cancellationToken)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }
}