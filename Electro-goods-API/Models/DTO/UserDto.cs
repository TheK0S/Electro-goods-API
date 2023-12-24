﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Electro_goods_API.Models.DTO
{
    public class UserDto
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
        public RoleDto? Role { get; set; }
        public BasketDto? Basket { get; set; }
        public List<OrderDto>? Orders { get; set; }
    }
}