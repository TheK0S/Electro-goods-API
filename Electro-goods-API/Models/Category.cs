using System.ComponentModel.DataAnnotations;

namespace Electro_goods_API.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryNameUK { get; set; }
    }
}
