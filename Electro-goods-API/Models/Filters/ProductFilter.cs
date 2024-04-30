namespace Electro_goods_API.Models.Filters
{
    public class ProductFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public List<int>? CategoryIds { get; set; }
        public List<int>? CountryIds { get; set; }
        public List<int>? ManufacturerIds { get; set; }
        public string? PartOfName{ get; set; }
        public Dictionary<string,string>? ProductAttributesDict { get; set; }
    }
}
