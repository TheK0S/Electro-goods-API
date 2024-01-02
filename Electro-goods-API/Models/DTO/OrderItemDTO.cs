using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
    }
}
