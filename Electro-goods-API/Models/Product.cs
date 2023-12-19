using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public string? ProductNameUK { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductDescriptionUK { get; set; }
        public decimal ProductPrice { get; set; } = 0;
        public int ProductStockQuantity { get; set; }
        public int ProductDiscount { get; set; }
        public bool IsActive { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey(nameof(ManufacturerId))]
        public Manufacturer? Manufacturer { get; set;}
        public List<ProductAttributes>? ProductAttributes { get; set; }

    }
}
