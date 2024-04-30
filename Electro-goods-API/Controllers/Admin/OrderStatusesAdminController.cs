using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesAdminController : ControllerBase
    {
        private readonly IOrderStatusRepository _context;

        public OrderStatusesAdminController(IOrderStatusRepository context)
        {
            _context = context;
        }

        // GET: api/OrderStatusesAdmin
        [HttpGet]
        public async Task<ActionResult<List<OrderStatus>>> GetAllOrderStatuses()
        {
            return Ok(await _context.GetAllOrderStatuses());
        }

        // GET: api/OrderStatusesAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatusById(int id)
        {
            return Ok(await _context.GetOrderStatusById(id));
        }

        // PUT: api/OrderStatusesAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderStatus(int id, OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
                return BadRequest();

            await _context.UpdateOrderStatus(id, orderStatus);

            return NoContent();
        }

        // POST: api/OrderStatusesAdmin
        [HttpPost]
        public async Task<ActionResult<OrderStatus>> PostOrderStatus(OrderStatus orderStatus)
        {
            await _context.CreateOrderStatus(orderStatus);

            return CreatedAtAction("GetOrderStatusById", new { id = orderStatus.Id }, orderStatus);
        }

        // DELETE: api/OrderStatusesAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id)
        {
            await _context.DeleteOrderStatus(id);

            return NoContent();
        }
    }
}
