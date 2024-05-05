namespace Electro_goods_API.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameUK { get; set; }

        public List<Product>? Products { get; set; }
    }
}
