using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Application.DTOs;
using OrderService.Api.Application.Interfaces;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderApplicationService _service;

        public OrdersController(IOrderApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsync();

            if (result == null)
                return NotFound("Não há registros cadastrados!");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderRequest request)
        {
            var order = await _service.CreateAsync(request.CustomerName, request.Description, request.Value);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}
