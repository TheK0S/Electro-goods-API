using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Electro_goods_API.Mapping.Mapper;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _service;

        public OrdersController(IOrderRepository service)
        {
            _service = service;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _service.GetAllOrders();
            var ordersDto = MapOrderToOrderDTO(orders, GetLanguageFromHeaders(Request.Headers));
            return Ok(ordersDto);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            Order order = await _service.GetOrderById(id);

            return Ok(order);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Product product)
        {
            await _service.UpdateProduct(id, product);
            return NoContent();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Product>> PostOrder(Product product)
        {
            product = await _service.CreateProduct(product);

            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _service.DeleteProduct(id);
            return NoContent();
        }
    }
}
