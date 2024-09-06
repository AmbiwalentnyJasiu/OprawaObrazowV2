using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Services;

public class ClientService(IBaseRepository<Client> repo) : BaseService<Client>(repo)
{
    public override async Task Update(int key, Client entity)
    {
        if (key != entity.Id)
        {
            throw new Exception("Inconsistent Id");
        }
        
        await repo.Update(entity);
    }
    
}