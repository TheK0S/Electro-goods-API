using Electro_goods_API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingAddress { get; set; }
        public string? OrderStatus { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
