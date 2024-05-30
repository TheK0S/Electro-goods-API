using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts([FromQuery] ProductFilter filter)
        {
            var products = await _service.GetProducts(filter);
            var productsDto = _mapper.MapProductToProductDTO(products);
            return Ok(productsDto);
        }

        // GET: api/Products/byProductIds
        [HttpGet("byProductIds")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsByIds([FromQuery] List<int> productIds)
        {
            var products = await _service.GetProductsByProductIds(productIds);
            var productsDto = _mapper.MapProductToProductDTO(products);
            return Ok(productsDto);
        }

        // GET: api/Products/count
        [HttpGet("count")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsCount([FromQuery] ProductFilter filter)
        {
            return Ok(await _service.GetProductsCount(filter));
        }

        // GET: api/Products/discounted
        [HttpGet("discounted")]
        public async Task<ActionResult<List<ProductDTO>>> GetDiscountedProducts(int page, int pageSize)
        {
            var products = await _service.GetDiscountedProducts(page, pageSize);
            var productDTO = _mapper.MapProductToProductDTO(products);

            return Ok(productDTO);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            Product product = await _service.GetProductById(id);
            ProductDTO productDTO = _mapper.MapProductToProductDTO(product);
            return Ok(productDTO);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            await _service.UpdateProduct(id, product);
            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product = await _service.CreateProduct(product);

            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service.DeleteProduct(id);
            return NoContent();
        }
    }
}
