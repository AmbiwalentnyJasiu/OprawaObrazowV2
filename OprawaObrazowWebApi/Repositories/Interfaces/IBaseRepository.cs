namespace OprawaObrazowWebApi.Repositories.Interfaces;

public interface IBaseRepository<T> 
    where T: class
{
    public IQueryable<T> GetAll();
    public IQueryable<T> GetById(int id);
    public Task Create(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
}