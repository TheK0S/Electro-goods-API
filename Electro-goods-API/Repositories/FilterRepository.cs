using Electro_goods_API.Models;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;

namespace Electro_goods_API.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICountryRepositiry _countryRepositiry;
        private readonly IManufacturerRepository _manufacturerRepository;
        public FilterRepository(AppDbContext context, ICategoryRepository categoryRepository, ICountryRepositiry countryRepositiry, IManufacturerRepository manufacturerRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _countryRepositiry = countryRepositiry;
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<StaticFilter> GetStaticFilters()
        {
            var categories = await _categoryRepository.GetAllCategories();
            var countries = await _countryRepositiry.GetAllCountries();
            var manufacturers = await _manufacturerRepository.GetAllManufacturers();

            return new StaticFilter
            {
                Categories = categories,
                Countries = countries,
                Manufacturers = manufacturers
            };
        }

        public AttributeFilter GetProductAttributeFilters(ProductFilter filter)
        {
            var query = _context.Products
                .AsQueryable()
                .Where(p => p.IsActive)
                .SelectMany(p => p.ProductAttributes)
                .GroupBy(pa => pa.AttributeName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(pa => pa.AttributeValue).ToList()
                );
                     );
                return new AttributeFilter { AttributeFilters = attributes };
            }
            else
                return new AttributeFilter();
                return new AttributeFilter();

            //if (filter.MinPrice.HasValue)
            //    query = query.Where(p => p.Price >= filter.MinPrice);

            //if (filter.MaxPrice.HasValue && filter.MaxPrice > filter.MinPrice)
            //    query = query.Where(p => p.Price <= filter.MaxPrice);

            //if (filter.CategoryId.HasValue && filter.CategoryId > 0)
            //    query = query.Where(p => p.CategoryId == filter.CategoryId);

            //if (filter.CountryId.HasValue && filter.CountryId > 0)
            //    query = query.Where(p => p.CountryId == filter.CountryId);

            //if (filter.ManufacturerId.HasValue && filter.ManufacturerId > 0)
            //    query = query.Where(p => p.ManufacturerId == filter.ManufacturerId);

            //if (filter.ProductAttributesDict != null && filter.ProductAttributesDict.Any())
            //    foreach (var attribute in filter.ProductAttributesDict)
            //        query = query.Where(p => p.ProductAttributes.Any(pa =>
            //            (pa.AttributeName == attribute.Key || pa.AttributeNameUK == attribute.Key) &&
            //            (pa.AttributeValue == attribute.Value || pa.AttributeValueUK == attribute.Value)));

            //var skipAmount = (page - 1) * pageSize;
            //query = query.Skip(skipAmount).Take(pageSize);
            //query = query
            //    .Include(p => p.Category)
            //    .Include(p => p.Country)
            //    .Include(p => p.Manufacturer)
            //    .Include(p => p.ProductAttributes);

            //return await query.ToListAsync();
        }
    }
}
