using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.Entities
{
    public class ProductAttribute
    {
        [Key]
        public int AttributeId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public string? AttributeName { get; set; }
        public string? AttributeNameUK { get; set; }
        public string? AttributeValue { get; set; }
        public string? AttributeValueUK { get; set; }
    }
}
