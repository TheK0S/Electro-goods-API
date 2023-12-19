using Microsoft.Build.Framework;

namespace Electro_goods_API.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [Required]
        public string NameUA { get; set; }
        public string NameRU { get; set; }
        public List<Product> Products { get; set; }
    }
}
