using Electro_goods_API.Mapping;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Electro_goods_API.Mapping.Mapper;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _service;

        public ProductsController(IProductRepository service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            var products = await _service.GetAllProducts();
            var productsDto = MapProductToProductDTO(products, GetLanguageFromHeaders(Request.Headers));
            return Ok(productsDto);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            Product product = await _service.GetProductById(id);
            ProductDTO productDTO = MapProductToProductDTO(product, GetLanguageFromHeaders(Request.Headers));
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
