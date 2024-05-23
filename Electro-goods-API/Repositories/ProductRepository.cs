using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            var query = GetProductQueryByFilter(filter);        

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

            var query = GetProductQueryByFilter(filter);

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

        public async Task<List<Product>> GetProductsByProductIds(List<int> productIds)
        {
            if(productIds is null) throw new ArgumentNullException(nameof(productIds));

            return await _context.Products
                .Where(p => productIds
                .Contains(p.Id))
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

        public IQueryable<Product> GetProductQueryByFilter(ProductFilter filter)
        {
            var query = _context.Products.AsQueryable().Where(p => p.IsActive);

            if (!filter.PartOfName.IsNullOrEmpty())
                query = query.Where(p =>
                    p.Name.ToUpper().Contains(filter.PartOfName.ToUpper())
                    || p.NameUK.ToUpper().Contains(filter.PartOfName.ToUpper()));

            if (filter.MinPrice.HasValue && filter.MinPrice > 0)
                query = query.Where(p => p.Price >= filter.MinPrice);

            if (filter.MaxPrice.HasValue && filter.MaxPrice > filter.MinPrice)
                query = query.Where(p => p.Price <= filter.MaxPrice);

            if (filter.CategoryIds?.Count > 0)
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId));

            if (filter.CountryIds?.Count > 0)
                query = query.Where(p => filter.CountryIds.Contains(p.CountryId));

            if (filter.ManufacturerIds?.Count > 0)
                query = query.Where(p => filter.ManufacturerIds.Contains(p.ManufacturerId));

            if (filter.ProductAttrIds?.Count > 0)
            {
                //take productAttribute name and value by id
                var productAttributeList = _context.ProductAttributs
                    .Where(pa => filter.ProductAttrIds.Contains(pa.AttributeId))
                    .ToList();

                query = query.Where(p =>
                    p.ProductAttributes.Any(pa =>
                        productAttributeList.Contains(pa)
                    )
                );
            }

            if (filter.Page > 0 && filter.PageSize > 0)
            {
                var skipAmount = (filter.Page - 1) * filter.PageSize;
                query = query.Skip(skipAmount).Take(filter.PageSize);
            }

            return query;
        }
    }
}
