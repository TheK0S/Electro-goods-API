using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class OrderDTORequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public decimal Price { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingAddress { get; set; }
        public List<OrderItemDTO>? OrderItems { get; set; }
    }
}
