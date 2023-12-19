namespace Electro_goods_API.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set;}
        public string? ProductNameUK { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
