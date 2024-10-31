using MassTransit;
using Common.Interfaces;
using Domain.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Common.Models;

namespace Application.Consumers
{
    public class OrderForDeliveryConsumer : IConsumer<IOrderForDelivery>
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

        public async Task Consume(ConsumeContext<IOrderForDelivery> context)
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