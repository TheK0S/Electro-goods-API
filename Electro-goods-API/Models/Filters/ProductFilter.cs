using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.Filters
{
    public class ProductFilter
    {
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? CategoryId { get; set; }
        public int? CountryId { get; set; }
        public int? ManufacturerId { get; set; }
        public string? PartOfName{ get; set; }
        public Dictionary<string,string>? ProductAttributesDict { get; set; }
    }
}
