namespace Electro_goods_API.Models.DTO
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameUK { get; set; }

        public  List<ProductDto>? Products { get; set; }
    }
}
