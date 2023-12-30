using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string? ShippingAddress { get; set; }
        public int OrderStatusId { get; set; }
        public User? User { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
