using AutoMapper;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
        {
            return Ok(await _service.GetAllProducts());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            Product product = await _service.GetProductById(id);

            string? lang = Request.Headers.Keys.Contains("Api-Language") ? Request.Headers["Api-Language"] : "ru";

            ProductDTO productDTO = new()
            {
                Id = product.Id,
                Name = lang == "ru" ? product.Name : product.NameUK,
                Description = lang == "ru" ? product.Description : product.DescriptionUK,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CountryId = product.CountryId,
                ManufacturerId = product.ManufacturerId
            };

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
        public async Task<ActionResult<Product>> PostRole(Product product)
        {
            product = await _service.CreateProduct(product);

            return CreatedAtAction("GetProductById", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _service.DeleteProduct(id);
            return NoContent();
        }
    }
}
