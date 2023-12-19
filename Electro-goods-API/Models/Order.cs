namespace Electro_goods_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public DateOnly OrderDate{ get; set; }
        public double OrderPrice { get; set; }
        public string? ShippingAddress { get; set; }
        public int OrderStatusId { get; set; }
    }
}
