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
        public FiltersController(IFilterRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Filters/Static
        [HttpGet("static")]
        public async Task<ActionResult<List<dynamic>>> GetStaticFilters()
        {
            var staticFilter = await _context.GetStaticFilters();

            if (staticFilter == null)
                return NotFound();

            string language = _mapper.GetLanguageFromHeaders(Request.Headers);
            var staticFilters = new List<dynamic>
            {
                new StaticFilterDTO<CategoryDTO>
                {
                    Name = language == "ru"? "Категоря" : "Категорія",
                    RequestName = "categoryIds",
                    Items = _mapper.MapCategoryToCategoryDTO(staticFilter.Categories, language)
                },
                new StaticFilterDTO<CountryDTO>
                {
                    Name = language == "ru"? "Страна производителя" : "Країна виробника",
                    RequestName = "countryIds",
                    Items = _mapper.MapCountryToCountryDTO(staticFilter.Countries, language)
                },
                new StaticFilterDTO<ManufacturerDTO>
                {
                    Name = language == "ru"? "Производитель" : "Виробник",
                    RequestName = "manufacturerIds",
                    Items = _mapper.MapManufacturerToManufacturerDTO(staticFilter.Manufacturers, language)
                }
            };

            return staticFilters;
        }

        // GET: api/Filters/Dynamic
        [HttpGet("dynamic")]
        public ActionResult<AttributeFilter> GetDynamicFilters([FromQuery] ProductFilter filter)
        {
            return _context.GetProductAttributeFilters(filter);
        }
    }
}
