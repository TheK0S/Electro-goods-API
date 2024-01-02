using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string? ShippingAddress { get; set; }
        public User? User { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
