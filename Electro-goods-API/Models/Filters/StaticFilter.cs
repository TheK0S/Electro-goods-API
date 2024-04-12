using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.Filters
{
    public class StaticFilter
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<Manufacturer> Manufacturers { get; set; } = new List<Manufacturer>();
    }
}
