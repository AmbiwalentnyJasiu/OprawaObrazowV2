using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class FrameRepository(DatabaseContext context) : IBaseRepository<Frame>
{
    public IQueryable<Frame> GetAll()
    {
        return context.Frames
            .Include(f => f.FramePieces)
            .Include(f => f.Supplier)
            .AsQueryable();
    }

    public IQueryable<Frame> GetById(int id)
    {
        return GetAll()
            .Where(f => f.Id == id);
    }

    public async Task Create(Frame entity)
    {
        context.Frames.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(Frame entity)
    {
        context.Frames.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Frame entity)
    {
        context.Frames.Remove(entity);
        await context.SaveChangesAsync();
    }
}