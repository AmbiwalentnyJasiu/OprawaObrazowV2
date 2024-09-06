namespace OprawaObrazowWebApi.Services.Interfaces;

public interface IBaseService<T> where T: class
{
    Task<List<T>> GetAll();
    Task<T> GetById(int id);
    Task Create(T entity);
    Task Update(int key, T entity);
    Task Delete(int id);
}