using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAdminController : ControllerBase
    {
        private readonly IProductRepository _context;

        public ProductsAdminController(IProductRepository context)
        {
            _context = context;
        }

        // GET: api/ProductsAdmin
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await _context.GetAllProducts());
        }

        // GET: api/ProductsAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            return Ok(await _context.GetProductById(id));
        }

        // PUT: api/ProductsAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _context.UpdateProduct(id, product);

            return NoContent();
        }

        // POST: api/ProductsAdmin
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _context.CreateProduct(product);

            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        // DELETE: api/ProductsAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _context.DeleteProduct(id);

            return NoContent();
        }
    }
}
