using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.DTO
{
    public class ProductAttributeDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}
