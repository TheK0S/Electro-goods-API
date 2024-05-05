using Electro_goods_API.Comparers;
using Electro_goods_API.Models;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Electro_goods_API.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly AppDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICountryRepositiry _countryRepositiry;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IProductRepository _productRepository;
        public FilterRepository(AppDbContext context, ICategoryRepository categoryRepository, ICountryRepositiry countryRepositiry, IManufacturerRepository manufacturerRepository, IProductRepository productRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _countryRepositiry = countryRepositiry;
            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
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

        public Dictionary<string, List<ProductAttributeFilterItemDTO>> GetProductAttributeFilters(ProductFilter filter, string language)
        {
            language = language.IsNullOrEmpty() ? "ru" : language;

            filter.Page = 0;
            filter.PageSize = 0;

            var attributesRu = _productRepository.GetProductQueryByFilter(filter)
                .SelectMany(p => p.ProductAttributes)
                .GroupBy(pa => language == "ru" ? pa.AttributeName ?? "no name" : pa.AttributeNameUK ?? "no name")
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(pa => new ProductAttributeFilterItemDTO
                    {
                        Id = pa.AttributeId,
                        Value = language == "ru" ? pa.AttributeValue : pa.AttributeValueUK
                    }
                    )
                    .Distinct(new ProductAttributeFilterItemDTOComparer())
                    .ToList()
                    );

            return attributesRu;
        }
    }
}
