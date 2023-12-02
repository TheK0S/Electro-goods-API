using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models
{
    public class ProductAttributes
    {
        [ForeignKey(nameof(AttributeId))]
        public int AttributeId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey(nameof(LanguageId))]
        public Language Language { get; set; }
    }
}
