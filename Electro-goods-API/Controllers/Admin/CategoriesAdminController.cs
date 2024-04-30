using Microsoft.AspNetCore.Mvc;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAdminController : ControllerBase
    {
        private readonly ICategoryRepository _context;

        public CategoriesAdminController(ICategoryRepository context)
        {
            _context = context;
        }

        // GET: api/CategoriesAdmin
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
          return Ok(await _context.GetAllCategories());
        }

        // GET: api/CategoriesAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            return Ok(await _context.GetCategoryById(id));
        }

        // PUT: api/CategoriesAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            await _context.UpdateCategory(id, category);

            return NoContent();
        }

        // POST: api/CategoriesAdmin
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await _context.CreateCategory(category);

            return CreatedAtAction("GetCategoryById", new { id = category.Id }, category);
        }

        // DELETE: api/CategoriesAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _context.DeleteCategory(id);

            return NoContent();
        }
    }
}
