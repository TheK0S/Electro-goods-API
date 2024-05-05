namespace Electro_goods_API.Models.DTO
{
    public class StaticFilterDTO<T>
    {
        public string? Name {  get; set; }
        public string? RequestName { get; set; }
        public List<T>? Items { get; set; }
    }
}
