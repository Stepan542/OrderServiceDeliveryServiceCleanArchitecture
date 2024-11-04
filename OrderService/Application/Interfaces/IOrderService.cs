using Application.Dtos;
using Common.Infrastructure;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService 
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}