using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class OrderItemRequestDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDTO? Product { get; set; }
    }
}
