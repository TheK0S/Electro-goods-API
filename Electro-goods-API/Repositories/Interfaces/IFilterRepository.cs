using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IFilterRepository
    {
        Task<StaticFilter> GetStaticFilters();
        Task<AttributeFilter> GetProductAttributeFilters(ProductFilter filter);
    }
}
