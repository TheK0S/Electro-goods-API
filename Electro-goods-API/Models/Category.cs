using System.ComponentModel.DataAnnotations;

namespace Electro_goods_API.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string NameUA { get; set; }
        public string NameRU { get; set; }
    }
}
