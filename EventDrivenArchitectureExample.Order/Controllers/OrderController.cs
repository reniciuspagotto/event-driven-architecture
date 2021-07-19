using EventDrivenArchitectureExample.Order.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        protected readonly IOrderHandler _orderHandler;

        public OrderController(IOrderHandler orderHandler)
        {
            _orderHandler = orderHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Data.Entities.Order order)
        {
            var response = await _orderHandler.Create(order);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var response = await _orderHandler.GetById(id);
            return Ok(response);
        }
    }
}
