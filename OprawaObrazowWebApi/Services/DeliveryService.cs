using OprawaObrazowWebApi.Models;
using OprawaObrazowWebApi.Repositories.Interfaces;

namespace OprawaObrazowWebApi.Services;

public class DeliveryService(IBaseRepository<Delivery> repo) : BaseService<Delivery>(repo)
{
    public override async Task Create(Delivery entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        if (entity.SupplierId <= 0)
        {
            throw new ArgumentException("SupplierId must be greater than 0.", nameof(entity.SupplierId));
        }

        if (entity.DateDue <= DateTime.Today)
        {
            throw new ArgumentException("DateDue must be a future date.", nameof(entity.DateDue));
        }

        await base.Create(entity);
    }
}