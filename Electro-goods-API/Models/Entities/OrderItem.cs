using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductNameUK { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
