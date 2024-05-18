using OprawaObrazowWebApi.Repositories.Interfaces;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Services;

public class BaseService<T>(IBaseRepository<T> repo) : IBaseService<T> where T: class
{
    public virtual IQueryable<T> GetAll()
    {
        return repo.GetAll();
    }

    public virtual IQueryable<T> GetById(int id)
    {
        return repo.GetById(id);
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