using Microsoft.Build.Framework;

namespace Electro_goods_API.Models.DTO
{
    public class ManufacturerDto
    {
        public int Id { get; set; }
        public string? NameUA { get; set; }
        public string? NameRU { get; set; }

        public List<ProductDto>? Products { get; set; }
    }
}
