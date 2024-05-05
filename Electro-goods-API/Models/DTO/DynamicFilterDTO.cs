namespace Electro_goods_API.Models.DTO
{
    public class DynamicFilterDTO
    {
        public string? RequestName { get; set; }
        public Dictionary<string, List<ProductAttributeFilterItemDTO>> Items { get; set; }
    }
}
