using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _service;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> GetOrdersByUserId(int id)
        {
            var orders = await _service.GetOrdersByUserId(id);
            var ordersDto = _mapper.MapOrderToOrderDTO(orders, _mapper.GetLanguageFromHeaders(Request.Headers));
            return Ok(ordersDto);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(Order order)
        {
            order = await _service.CreateOrder(order);

            return Ok("Order is created");
        }
    }
}
