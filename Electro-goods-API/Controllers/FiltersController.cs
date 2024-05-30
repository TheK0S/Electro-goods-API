using Electro_goods_API.Mapping;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilterRepository _context;
        private readonly IMapper _mapper;
        private readonly string language;
        public FiltersController(IFilterRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            language = HttpContext?.Items["Language"]?.ToString() ?? "ru";
        }

        // GET: api/Filters/Static
        [HttpGet("static")]
        public async Task<ActionResult<List<dynamic>>> GetStaticFilters()
        {
            var staticFilter = await _context.GetStaticFilters();

            if (staticFilter == null)
                return NotFound();

            var staticFilters = new List<dynamic>
            {
                new StaticFilterDTO<CategoryDTO>
                {
                    Name = language == "ru"? "Категоря" : "Категорія",
                    RequestName = "categoryIds",
                    Items = _mapper.MapCategoryToCategoryDTO(staticFilter.Categories)
                },
                new StaticFilterDTO<CountryDTO>
                {
                    Name = language == "ru"? "Страна производителя" : "Країна виробника",
                    RequestName = "countryIds",
                    Items = _mapper.MapCountryToCountryDTO(staticFilter.Countries)
                },
                new StaticFilterDTO<ManufacturerDTO>
                {
                    Name = language == "ru"? "Производитель" : "Виробник",
                    RequestName = "manufacturerIds",
                    Items = _mapper.MapManufacturerToManufacturerDTO(staticFilter.Manufacturers)
                }
            };

            return staticFilters;
        }

        // GET: api/Filters/Dynamic
        [HttpGet("dynamic")]
        public ActionResult<DynamicFilterDTO> GetDynamicFilters([FromQuery] ProductFilter filter)
        {
            var attributes = _context.GetProductAttributeFilters(filter);

            if (attributes is null) return NotFound();

            DynamicFilterDTO dynamicFilter = new()
            {
                RequestName = "productAttrIds",
                Items = attributes
            };

            return dynamicFilter;
        }
    }
}
