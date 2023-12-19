namespace Electro_goods_API.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }
}
