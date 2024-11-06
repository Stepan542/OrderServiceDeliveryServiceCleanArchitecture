using Application.Interfaces;
using Domain.Interfaces;
using Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Application.Dtos;
using MassTransit;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        // попробуй без OrderService в logger
        private readonly ILogger<OrderService> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderService> logger, 
            IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
           var orders = await _orderRepository.GetAllAsync();
           return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            _logger.LogInformation($"Текущий Id заказа: {id}");
            return order;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            _logger.LogInformation("recieved an order: {@Order}", order);

            var createdOrder = await _orderRepository.CreateAsync(order);

            try {
                await _publishEndpoint.Publish<OrderDto>(createdOrder);
                _logger.LogInformation("published: {@Order}", order);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "an error occured while sending the event.");
            }

            return createdOrder;
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            var currentOrder = await _orderRepository.GetByIdAsync(order.Id);
            if (currentOrder == null) 
            {
                _logger.LogInformation("Текущий объект  пуст");

                return false;
            }

            _logger.LogInformation($"Текущий Id заказа: {order.Id}");
            _logger.LogInformation($"{currentOrder.Id}, {currentOrder.Name}, {currentOrder.Price}");
            
            if (currentOrder == null) return false;
            _logger.LogInformation($"До Map: {currentOrder.Name}, {currentOrder.Quantity}, {currentOrder.Price}");
            // работает только явное преобразование
            currentOrder.Name = order.Name;
            currentOrder.Quantity = order.Quantity;
            currentOrder.Price = order.Price;
            // Map не преобразует данные
            // _mapper.Map(order, currentOrder);
            _logger.LogInformation($"После Map: {currentOrder.Name}, {currentOrder.Quantity}, {currentOrder.Price}");

            await _orderRepository.UpdateAsync(currentOrder);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}