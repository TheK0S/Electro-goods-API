using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.Filters
{
    public class ProductFilter
    {
        public string? NameContains { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Discount { get; set; }
        public int? CategoryId { get; set; }
        public int? CountryId { get; set; }
        public int? ManufacturerId { get; set; }
        public Dictionary<string,string>? ProductAttributesDict { get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set;}
    }
}
