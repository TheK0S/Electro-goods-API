﻿using System.ComponentModel.DataAnnotations;

namespace Electro_goods_API.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
