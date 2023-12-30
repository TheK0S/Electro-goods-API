namespace Electro_goods_API.Models.DTO
{
    public class ProductRuDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Discount { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
        public int ManufacturerId { get; set; }
    }
}
