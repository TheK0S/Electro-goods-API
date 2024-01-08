using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
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
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId,
                ProductAttributesDict = attributes
            };

            var products = await _service.GetProducts(page, pageSize, filter);
            var productsDto = _mapper.MapProductToProductDTO(products, _mapper.GetLanguageFromHeaders(Request.Headers));
            return Ok(productsDto);
        }

        // GET: api/Products/count
        [HttpGet("count")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductsCount(
            [FromQuery] int minPrice,
            [FromQuery] int maxPrice,
            [FromQuery] int categoryId,
            [FromQuery] int manufacturerId,
            [FromQuery] Dictionary<string, string> attributes)
        {
            if (attributes.ContainsKey("minPrice")) attributes.Remove("minPrice");
            if (attributes.ContainsKey("maxPrice")) attributes.Remove("maxPrice");
            if (attributes.ContainsKey("categoryId")) attributes.Remove("categoryId");
            if (attributes.ContainsKey("manufacturerId")) attributes.Remove("manufacturerId");

            ProductFilter filter = new ProductFilter
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId,
                ProductAttributesDict = attributes
            };
                        
            return Ok(await _service.GetProductsCount(filter));
        }

        // GET: api/Products/discounted
        [HttpGet("discounted")]
        public async Task<ActionResult<List<ProductDTO>>> GetDiscountedProducts(int page, int pageSize)
        {
            var products = await _service.GetDiscountedProducts(page, pageSize);
            var productDTO = _mapper.MapProductToProductDTO(products, _mapper.GetLanguageFromHeaders(Request.Headers));

            return Ok(productDTO);
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
