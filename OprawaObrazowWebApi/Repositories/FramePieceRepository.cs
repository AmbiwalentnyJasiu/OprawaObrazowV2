using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Repositories;

public class FramePieceRepository(DatabaseContext context) : IBaseRepository<FramePiece>
{
    public IQueryable<FramePiece> GetAll()
    {
        return context.FramePieces
            .Include(fp => fp.Frame)
            .Include(fp => fp.Delivery)
            .Include(fp => fp.Order)
            .AsQueryable();
    }

    public IQueryable<FramePiece> GetById(int id)
    {
        return GetAll()
            .Where(fp => fp.Id == id);
    }

    public async Task Create(FramePiece entity)
    {
        context.FramePieces.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task Update(FramePiece entity)
    {
        context.FramePieces.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task Delete(FramePiece entity)
    {
        context.FramePieces.Remove(entity);
        await context.SaveChangesAsync();
    }
}