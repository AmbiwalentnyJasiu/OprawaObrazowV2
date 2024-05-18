using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class SupplierRepository(DatabaseContext context) : IBaseRepository<Supplier>
{
    public IQueryable<Supplier> GetAll()
    {
        return context.Suppliers
            .Include(s => s.Deliveries)
            .Include(s => s.Frames)
            .AsQueryable();
    }

    public IQueryable<Supplier> GetById(int id)
    {
        return GetAll()
            .Where(s => s.Id == id);
    }

    public async Task Create(Supplier entity)
    {
        context.Suppliers.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Supplier entity)
    {
        context.Suppliers.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Supplier entity)
    {
        context.Suppliers.Remove(entity);
        await context.SaveChangesAsync();
    }
}