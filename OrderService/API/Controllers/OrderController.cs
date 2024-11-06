using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using AutoMapper;
using MassTransit;
using Domain.Entities;
using Application.Dtos;
using MassTransit.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateAsync(order);
            return Ok(createdOrder);
        }

        // исправить PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] Order order)
        {
            order.Id = id;
            if (order == null) return NotFound();
            await _orderService.UpdateAsync(order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok();
        }
    }   
}