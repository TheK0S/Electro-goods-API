using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersAdminController : ControllerBase
    {
        private readonly IOrderRepository _context;

        public OrdersAdminController(IOrderRepository context)
        {
            _context = context;
        }

        // GET: api/OrdersAdmin
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders(OrderFilter filter)
        {
            return Ok(await _context.GetOrders(filter));
        }

        // GET: api/OrdersAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            return Ok(await _context.GetOrderById(id));
        }

        // PUT: api/OrdersAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
                return BadRequest();

            await _context.UpdateOrder(id, order);

            return NoContent();
        }

        // POST: api/OrdersAdmin
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            await _context.CreateOrder(order);

            return CreatedAtAction("GetOrderById", new { id = order.Id }, order);
        }

        // DELETE: api/OrdersAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _context.DeleteOrder(id);

            return NoContent();
        }
    }
}
