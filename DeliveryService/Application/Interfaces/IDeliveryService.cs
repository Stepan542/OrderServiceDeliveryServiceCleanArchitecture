using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDeliveryService
    {
        Task CreateAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
    }
}