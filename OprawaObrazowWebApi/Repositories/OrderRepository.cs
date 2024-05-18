using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class OrderRepository(DatabaseContext context) : IBaseRepository<Order>
{
    public IQueryable<Order> GetAll()
    {
        return context.Orders
            .Include(d => d.FramePieces)
            .Include(d => d.Client)
            .AsQueryable();
    }

    public IQueryable<Order> GetById(int id)
    {
        return GetAll()
            .Where(o => o.Id == id);
    }

    public async Task Create(Order entity)
    {
        context.Orders.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Order entity)
    {
        context.Orders.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Order entity)
    {
        context.Orders.Remove(entity);
        await context.SaveChangesAsync();
    }
}