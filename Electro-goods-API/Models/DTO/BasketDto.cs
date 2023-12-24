namespace Electro_goods_API.Models.DTO
{
    public class BasketDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDto? User { get; set; }
        public List<BasketItemDto>? BasketItems { get; set; }
    }
}
