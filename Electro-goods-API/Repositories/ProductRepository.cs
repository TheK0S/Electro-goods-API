using Electro_goods_API.Exceptions;
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
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts(ProductFilter filter)
        {
            if(filter == null)
                return await GetAllProducts();

            if (filter.Page < 1) filter.Page = 1;
            if (filter.PageSize < 1) filter.PageSize = 10;

            var query = _context.Products.AsQueryable().Where(p => p.IsActive);

            if(filter.MinPrice.HasValue && filter.MinPrice > 0)
                query = query.Where(p => p.Price >= filter.MinPrice);

            if(filter.MaxPrice.HasValue && filter.MaxPrice > filter.MinPrice)
                query = query.Where(p => p.Price <= filter.MaxPrice);

            if (filter.CategoryId?.Count > 0)
                query = query.Where(p => filter.CategoryId.Contains(p.CategoryId));

            if (filter.CountryId?.Count > 0)
                query = query.Where(p => filter.CountryId.Contains(p.CountryId));

            if (filter.ManufacturerId?.Count > 0)
                query = query.Where(p => filter.ManufacturerId.Contains(p.ManufacturerId));

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

            return await query.ToListAsync();
        }

        public async Task<int> GetProductsCount(ProductFilter filter)
        {
            if (filter == null)
                return await GetAllProductsCount();

            var query = _context.Products.AsQueryable().Where(p => p.IsActive);

            if (filter.MinPrice.HasValue && filter.MinPrice > 0)
                query = query.Where(p => p.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue && filter.MaxPrice > filter.MinPrice)
                query = query.Where(p => p.Price <= filter.MaxPrice);

            if (filter.CategoryId?.Count > 0)
                query = query.Where(p => filter.CategoryId.Contains(p.CategoryId));

            if (filter.CountryId?.Count > 0)
                query = query.Where(p => filter.CountryId.Contains(p.CountryId));

            if (filter.ManufacturerId?.Count > 0)
                query = query.Where(p => filter.ManufacturerId.Contains(p.ManufacturerId));

            if (filter.ProductAttributesDict != null && filter.ProductAttributesDict.Any())
                foreach (var attribute in filter.ProductAttributesDict)
                    query = query.Where(p => p.ProductAttributes.Any(pa =>
                        (pa.AttributeName == attribute.Key || pa.AttributeNameUK == attribute.Key) &&
                        (pa.AttributeValue == attribute.Value || pa.AttributeValueUK == attribute.Value)));

            return await query.CountAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Country)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.ProductAttributes)
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        NameUK = p.NameUK,
                        Description = p.Description,
                        DescriptionUK = p.DescriptionUK,
                        ImgPath = p.ImgPath,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        Discount = p.Discount,
                        IsActive = p.IsActive,
                        CategoryId = p.CategoryId,
                        CountryId = p.CountryId,
                        ManufacturerId = p.ManufacturerId,
                        Category = new Category
                        {
                            Id = p.Category.Id,
                            Name = p.Category.Name,
                            NameUK = p.Category.NameUK
                        },
                        Country = new Country
                        {
                            Id = p.Country.Id,
                            Name = p.Country.Name,
                            NameUK = p.Country.NameUK
                        },
                        Manufacturer = new Manufacturer
                        {
                            Id = p.Manufacturer.Id,
                            Name = p.Manufacturer.Name,
                            NameUK = p.Manufacturer.NameUK
                        },
                        ProductAttributes = p.ProductAttributes.ToList()
                    })
                    .ToListAsync();
        }

        public async Task<int> GetAllProductsCount()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<Product>> GetDiscountedProducts(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var skipAmount = (page - 1) * pageSize;
            var query = _context.Products.Where(p => p.Discount > 0).AsQueryable();
            query = query.Skip(skipAmount).Take(pageSize);

            return await _context.Products
                    .Where(p => p.Discount > 0)
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var product = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Country)
                    .Include(p => p.Manufacturer)
                    .Include(p => p.ProductAttributes)
                    .Select(p => new Product
                    {
                        Id = p.Id,
                        Name = p.Name,
                        NameUK = p.NameUK,
                        Description = p.Description,
                        DescriptionUK = p.DescriptionUK,
                        ImgPath = p.ImgPath,
                        Price = p.Price,
                        StockQuantity = p.StockQuantity,
                        Discount = p.Discount,
                        IsActive = p.IsActive,
                        CategoryId = p.CategoryId,
                        CountryId = p.CountryId,
                        ManufacturerId = p.ManufacturerId,
                        Category = new Category
                        {
                            Id = p.Category.Id,
                            Name = p.Category.Name,
                            NameUK = p.Category.NameUK
                        },
                        Country = new Country
                        {
                            Id = p.Country.Id,
                            Name = p.Country.Name,
                            NameUK = p.Country.NameUK
                        },
                        Manufacturer = new Manufacturer
                        {
                            Id = p.Manufacturer.Id,
                            Name = p.Manufacturer.Name,
                            NameUK = p.Manufacturer.NameUK
                        },
                        ProductAttributes = p.ProductAttributes.ToList()
                    })
                    .FirstAsync(p => p.Id == id);

            if (product == null)
                throw new NotFoundException($"Product with id={id} not found");

            return product;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new NotFoundException($"Product with id={id} not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
