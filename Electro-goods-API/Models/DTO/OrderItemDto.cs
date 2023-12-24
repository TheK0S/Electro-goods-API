namespace Electro_goods_API.Models.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int Quantity { get; set; }
        public OrderDto? Order { get; set; }
        public ProductDto? Product { get; set; }
    }
}
