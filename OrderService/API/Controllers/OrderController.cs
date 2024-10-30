using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using AutoMapper;
using MassTransit;
using Common.Models;
using Application.Dtos;
using Common.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, IMapper mapper, IPublishEndpoint publishEndpoint, 
            ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Order order)
        {
            _logger.LogInformation("recieved an order: {@Order}", order);

            var createdOrder = await _orderService.CreateAsync(order);

            try {
                await _publishEndpoint.Publish<IOrderForDelivery>(createdOrder);
                _logger.LogInformation("published: {@Order}", order);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "an error occured while sending the event.");
            }

            // если async будет в конце, возможна ошибка:
            // System.InvalidOperationException: No route matches the supplied values.
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody] Order order)
        {
            if (id != order.Id) return BadRequest();
            await _orderService.UpdateAsync(order);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _orderService.DeleteAsync(id);

            return NoContent();
        }
    }   
}