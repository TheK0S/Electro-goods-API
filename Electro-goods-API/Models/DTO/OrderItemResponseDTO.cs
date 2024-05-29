using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class OrderItemResponseDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
