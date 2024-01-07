using Azure.Core.GeoJson;
using Electro_goods_API.Mapping;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

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

        // POST: api/Products
        //[HttpPost("filter")]
        //public async Task<ActionResult<List<ProductDTO>>> GetProducts(ProductFilter filter)
        //{
        //    var products = await _service.GetProducts(filter);
        //    var productsDto = _mapper.MapProductToProductDTO(products, _mapper.GetLanguageFromHeaders(Request.Headers));
        //    return Ok(productsDto);
        //}

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts(
            [FromQuery] int page,
            [FromQuery] int pageSize,
            [FromQuery] int minPrice,
            [FromQuery] int maxPrice,
            [FromQuery] int categoryId,
            [FromQuery] int manufacturerId,
            [FromQuery] Dictionary<string, string> attributes)
        {
            if (attributes.ContainsKey("page")) attributes.Remove("page");
            if (attributes.ContainsKey("pageSize")) attributes.Remove("pageSize");
            if (attributes.ContainsKey("minPrice")) attributes.Remove("minPrice");
            if (attributes.ContainsKey("maxPrice")) attributes.Remove("maxPrice");
            if (attributes.ContainsKey("categoryId")) attributes.Remove("categoryId");
            if (attributes.ContainsKey("manufacturerId")) attributes.Remove("manufacturerId");

            ProductFilter filter = new ProductFilter
            {
                Page = page,
                PageSize = pageSize,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId,
                ProductAttributesDict = attributes
            };

            var products = await _service.GetProducts(filter);
            var productsDto = _mapper.MapProductToProductDTO(products, _mapper.GetLanguageFromHeaders(Request.Headers));
            return Ok(productsDto);
        }



        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            Product product = await _service.GetProductById(id);
            ProductDTO productDTO = _mapper.MapProductToProductDTO(product, _mapper.GetLanguageFromHeaders(Request.Headers));
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
