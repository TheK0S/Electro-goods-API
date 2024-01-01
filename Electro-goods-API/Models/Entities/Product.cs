using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? NameUK { get; set; }
        public string? Description { get; set; }
        public string? DescriptionUK { get; set; }
        public string? ImgPath { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; } = 0;
        public int StockQuantity { get; set; }
        public int Discount { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }
        public int ManufacturerId { get; set; }

        public Category? Category { get; set; }
        public Country? Country { get; set; }
        public Manufacturer? Manufacturer { get; set; }
        public List<ProductAttribut>? ProductAttributes { get; set; }

    }
}
