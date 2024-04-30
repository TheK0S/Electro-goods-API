using System.ComponentModel.DataAnnotations;

namespace Electro_goods_API.Models.DTO
{
    public class ChangePasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }
    }
}
