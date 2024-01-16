using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;

namespace Electro_goods_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            try
            {
                var category = await _context.Categories.FindAsync(id);

                if (category == null)
                    throw new NotFoundException($"Country with id={id} not found");

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Category> CreateCategory(Category category)
        {
            if (_context.Categories == null)
                throw new NotFoundException("Categories table not found");

            _context.Categories.Add(category);

            try
            {
                await _context.SaveChangesAsync();
                return category;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CategoryExists(id))
                    throw new NotFoundException($"Country with id={id} not found");

                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteCategory(int id)
        {
            if (_context.Categories == null)
                throw new NotFoundException("Categories table not found");

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                throw new NotFoundException($"Category with id={id} not found");

            _context.Categories.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
