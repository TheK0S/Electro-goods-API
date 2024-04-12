using Electro_goods_API.Models;
using Electro_goods_API.Models.Filters;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<AttributeFilter> GetProductAttributeFilters(ProductFilter filter)
        {
            return new AttributeFilter();
        }
    }
}
