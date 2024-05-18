using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class ClientRepository(DatabaseContext context) : IBaseRepository<Client>
{
    public IQueryable<Client> GetAll()
    {
        return context.Clients
            .Include(c => c.Orders)
            .AsQueryable();
    }

    public IQueryable<Client> GetById(int id)
    {
        return GetAll()
            .Where(c => c.Id == id);
    }

    public async Task Create(Client entity)
    {
        context.Clients.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Client entity)
    {
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Client entity)
    {
        context.Clients.Remove(entity);
        await context.SaveChangesAsync();
    }
}