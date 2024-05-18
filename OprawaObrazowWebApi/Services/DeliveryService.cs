using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Services;

public class DeliveryService(IBaseRepository<Delivery> repo) : BaseService<Delivery>(repo)
{
}