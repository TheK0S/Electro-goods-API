using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilterRepository _context;
        private readonly IMapper _mapper;
        public FiltersController(IFilterRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Filters/Static
        [HttpGet("static")]
        public async Task<ActionResult<StaticFilterDTO>> GetStaticFilters()
        {
            var staticFilter = await _context.GetStaticFilters();

            if (staticFilter == null)
                return NotFound();

            string language = _mapper.GetLanguageFromHeaders(Request.Headers);
            var staticFilterDTO = new StaticFilterDTO
            {
                Categories = _mapper.MapCategoryToCategoryDTO(staticFilter.Categories, language),
                Countries = _mapper.MapCountryToCountryDTO(staticFilter.Countries, language),
                Manufacturers = _mapper.MapManufacturerToManufacturerDTO(staticFilter.Manufacturers, language)
            };

            return staticFilterDTO;
        }

        // GET: api/Filters/Dynamic
        [HttpGet("dynamic")]
        public async Task<ActionResult<AttributeFilter>> GetDynamicFilters(
            [FromQuery] int minPrice,
            [FromQuery] int maxPrice,
            [FromQuery] int categoryId,
            [FromQuery] int manufacturerId,
            [FromQuery] string partOfName,
            [FromQuery] Dictionary<string, string> attributes)
        {
            if (attributes.ContainsKey("page")) attributes.Remove("page");
            if (attributes.ContainsKey("pageSize")) attributes.Remove("pageSize");
            if (attributes.ContainsKey("minPrice")) attributes.Remove("minPrice");
            if (attributes.ContainsKey("maxPrice")) attributes.Remove("maxPrice");
            if (attributes.ContainsKey("categoryId")) attributes.Remove("categoryId");
            if (attributes.ContainsKey("partOfName")) attributes.Remove("partOfName");
            if (attributes.ContainsKey("manufacturerId")) attributes.Remove("manufacturerId");

            ProductFilter filter = new ProductFilter
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId,
                PartOfName = partOfName,
                ProductAttributesDict = attributes
            };
            
            return await _context.GetProductAttributeFilters(filter);
        }
    }
}
