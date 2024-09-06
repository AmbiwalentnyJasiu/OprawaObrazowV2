using Microsoft.EntityFrameworkCore;
using OprawaObrazowWebApi.Repositories.Interfaces;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Services;

public class BaseService<T>(IBaseRepository<T> repo) : IBaseService<T> where T: class
{
    public virtual async Task<List<T>> GetAll()
    {
        return await repo.GetAll().ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        return await repo.GetById(id).FirstOrDefaultAsync() ?? throw new KeyNotFoundException();
    }

    public virtual async Task Create(T entity)
    {
        await repo.Create(entity);
    }

    public virtual async Task Update(int key, T entity)
    {
        await repo.Update(entity);
    }

    public virtual async Task Delete(int id)
    {
        var entity = repo.GetById(id).FirstOrDefault();

        if (entity is not null)
        {
            await repo.Delete(entity);
        }
    }
}