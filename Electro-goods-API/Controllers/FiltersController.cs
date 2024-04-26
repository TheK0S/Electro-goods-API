﻿using Electro_goods_API.Mapping.Interfaces;
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
                Category = _mapper.MapCategoryToCategoryDTO(staticFilter.Categories, language),
                Country = _mapper.MapCountryToCountryDTO(staticFilter.Countries, language),
                Manufacturer = _mapper.MapManufacturerToManufacturerDTO(staticFilter.Manufacturers, language)
            };

            return staticFilterDTO;
        }

        // GET: api/Filters/Dynamic
        [HttpGet("dynamic")]
        public async Task<ActionResult<AttributeFilter>> GetDynamicFilters([FromQuery] ProductFilter filter)
        {
            return await _context.GetProductAttributeFilters(filter);
        }
    }
}