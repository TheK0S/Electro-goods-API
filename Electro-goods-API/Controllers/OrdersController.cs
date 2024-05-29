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
        public async Task<ActionResult<List<OrderRequestDTO>>> GetOrdersByUserId(int id)
        {
            var orders = await _service.GetOrdersByUserId(id);
            string language = _mapper.GetLanguageFromHeaders(Request.Headers);
            var ordersDto = _mapper.MapOrderToOrderResponseDTO(orders, language);
            return Ok(ordersDto);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult> PostOrder(OrderRequestDTO orderRequest)
        {
            await _service.CreateOrder(orderRequest);

            return Ok("Order is created");
        }
    }
}
