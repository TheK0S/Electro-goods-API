﻿using System.ComponentModel.DataAnnotations;

namespace Electro_goods_API.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
