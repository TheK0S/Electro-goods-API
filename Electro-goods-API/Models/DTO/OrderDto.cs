using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string? ShippingAddress { get; set; }
        public int OrderStatusId { get; set; }
        public UserDto? User { get; set; }
        public OrderStatusDto? OrderStatus { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
    }
}
