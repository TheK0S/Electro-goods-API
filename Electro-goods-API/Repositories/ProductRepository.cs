using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetProducts(ProductFilter filter)
        {
            if(filter == null)
                return await GetAllProducts();

            var query = _context.Products.AsQueryable().Where(p => p.IsActive);

            if(!string.IsNullOrEmpty(filter.NameContains))
                query = query.Where(p => p.Name.ToLower().Contains(filter.NameContains.ToLower()) || p.NameUK.ToLower().Contains(filter.NameContains.ToLower()));

            if(filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice);

            if(filter.MaxPrice.HasValue && filter.MaxPrice > filter.MinPrice)
                query = query.Where(p => p.Price <= filter.MaxPrice);

            if(filter.Discount.HasValue && filter.Discount > 0)
                query = query.Where(p => p.Discount >= filter.Discount);

            if (filter.CategoryId.HasValue && filter.CategoryId > 0)
                query = query.Where(p => p.CategoryId == filter.CategoryId);

            if (filter.CountryId.HasValue && filter.CountryId > 0)
                query = query.Where(p => p.CountryId == filter.CountryId);

            if (filter.ManufacturerId.HasValue && filter.ManufacturerId > 0)
                query = query.Where(p => p.ManufacturerId == filter.ManufacturerId);

            if (filter.ProductAttributesDict != null && filter.ProductAttributesDict.Any())
                foreach (var attribute in filter.ProductAttributesDict)
                    query = query.Where(p => p.ProductAttributes.Any(pa => 
                        (pa.AttributeName == attribute.Key || pa.AttributeNameUK == attribute.Key) && 
                        (pa.AttributeValue == attribute.Value || pa.AttributeValueUK == attribute.Value)));

            var skipAmount = (filter.Page - 1) * filter.PageSize;
            query = query.Skip(skipAmount).Take(filter.PageSize);
            query = query
                .Include(p => p.Category)
                .Include(p => p.Country)
                .Include(p => p.Manufacturer)
                .Include(p => p.ProductAttributes);

            try
            {
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            try
            {
                var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Country)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.ProductAttributes)
                    .FirstAsync(p => p.Id == id);

                if (product == null)
                    throw new InvalidOperationException("Product not found");

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (_context.Products == null)
            {
                throw new InvalidOperationException("Products table not found");
            }

            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
                return product;
            }
            catch (DbUpdateConcurrencyException ex)
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

        public async Task UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductExists(id))
                    throw new InvalidOperationException("Product not found");

                _logger.LogError(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                throw new InvalidOperationException("Products table not found");
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ProductExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
