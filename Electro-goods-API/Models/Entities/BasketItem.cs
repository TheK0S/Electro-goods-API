using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductNameUK { get; set; }
        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public Basket? Basket { get; set; }
        public Product? Product { get; set; }
    }
}
