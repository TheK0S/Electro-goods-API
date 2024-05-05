namespace Electro_goods_API.Models.Filters
{
    public class AttributeFilter
    {
        public Dictionary<string, List<string>> AttributeFilters { get; set; } = new Dictionary<string, List<string>>();
    }
}
