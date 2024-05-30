using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _service;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _service.GetAllCategories();
            var categoriesDto = _mapper.MapCategoryToCategoryDTO(categories);
            return Ok(categoriesDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryById(id);
            var categoryDto = _mapper.MapCategoryToCategoryDTO(category);
            return Ok(categoryDto);
        }
    }
}
