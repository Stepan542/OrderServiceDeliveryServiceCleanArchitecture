using Domain.Interfaces;
using Common.Models;

namespace Application.Services
{
    public class DeliveryOrderService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryOrderService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task CreateAsync(Order order)
        {
            await _deliveryRepository.CreateAsync(order);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _deliveryRepository.GetByIdAsync(id);
        }
    }
}