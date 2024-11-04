using Common.Infrastructure;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeliveryRepository : IBaseRepository<Order> {}
}