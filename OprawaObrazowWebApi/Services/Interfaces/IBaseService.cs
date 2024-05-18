namespace OprawaObrazowWebApi.Services.Interfaces;

public interface IBaseService<T> where T: class
{
    IQueryable<T> GetAll();
    IQueryable<T> GetById(int id);
    Task Create(T entity);
    Task Update(int key, T entity);
    Task Delete(int id);
}