using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgPath { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public CategoryDTO? Category { get; set; }
        public CountryDTO? Country { get; set; }
        public ManufacturerDTO? Manufacturer { get; set; }
        public List<ProductAttributeDTO>? ProductAttributes { get; set; }
    }
}
