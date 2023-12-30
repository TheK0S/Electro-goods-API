using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string? FirstName { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string? LastName { get; set; }
        [MaxLength(50), MinLength(3)]
        public string? Patronomic { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public Basket? Basket { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
