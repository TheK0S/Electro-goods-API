using Electro_goods_API.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Electro_goods_API.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronomic { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public List<OrderResponseDTO>? Orders { get; set; }
    }
}
