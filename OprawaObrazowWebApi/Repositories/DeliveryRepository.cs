using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class DeliveryRepository(DatabaseContext context) : IBaseRepository<Delivery>
{
    public IQueryable<Delivery> GetAll()
    {
        return context.Deliveries
            .Include(d => d.FramePieces)
            .Include(d => d.Supplier)
            .AsQueryable();
    }

    public IQueryable<Delivery> GetById(int id)
    {
        return GetAll()
            .Where(d => d.Id == id);
    }

    public async Task Create(Delivery entity)
    {
        context.Deliveries.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Delivery entity)
    {
        context.Deliveries.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Delivery entity)
    {
        context.Deliveries.Remove(entity);
        await context.SaveChangesAsync();
    }
}