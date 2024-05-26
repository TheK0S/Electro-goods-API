﻿using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Models.DTO
{
    public class OrderDTOResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingAddress { get; set; }
        public int OrderStatusId { get; set; }
        public UserDTO? User { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
