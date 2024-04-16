using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly AppDbContext _context;
        public ProductAttributeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductAttribute>> GetProductAttributesByProductId(int productId)
        {
            return await _context.ProductAttributs
                .Where(productAttribute => productAttribute.ProductId == productId)
                .ToListAsync();
        }


        public async Task<ProductAttribute> GetProductAttributeById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var productAttribute = await _context.ProductAttributs.FindAsync(id);
            if (productAttribute == null)
                throw new NotFoundException($"Role with id={id} not found");

            return productAttribute;
        }


        public async Task<ProductAttribute> CreateProductAttribute(ProductAttribute productAttribute)
        {
            _context.ProductAttributs.Add(productAttribute);
            await _context.SaveChangesAsync();
            return productAttribute;
        }

        public async Task UpdateProductAttribute(int id, ProductAttribute productAttribute)
        {
            _context.Entry(productAttribute).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAttribute(int id)
        {
            var productAttribute = await _context.ProductAttributs.FindAsync(id);
            if (productAttribute == null)
                throw new NotFoundException($"OrderStatus with id={id} not found");

            _context.ProductAttributs.Remove(productAttribute);
            await _context.SaveChangesAsync();
        }
    }
}
