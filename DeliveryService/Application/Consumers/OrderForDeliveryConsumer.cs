using MassTransit;
using Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Application.Dtos;

namespace Application.Consumers
{
    // если использовать модель Order вместо OrderDto, работать не будет
    public class OrderForDeliveryConsumer : IConsumer<OrderDto>
    {
        private readonly IDeliveryService _deliveryService;
        private readonly ILogger<OrderForDeliveryConsumer> _logger;
        private readonly IMapper _mapper;

        public OrderForDeliveryConsumer(IDeliveryService deliveryService, ILogger<OrderForDeliveryConsumer> logger,
            IMapper mapper)
        {
            _deliveryService = deliveryService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<OrderDto> context)
        {
            var deliveryOrder = _mapper.Map<Order>(context.Message);

            try {
                await _deliveryService.CreateAsync(deliveryOrder);
                _logger.LogInformation("the event was successfully processed.");
            }

            catch(Exception ex) {
                _logger.LogError(ex, "an error occured while POST request");
                throw;
            }
            
        }
    }
}