namespace Electro_goods_API.Models.DTO
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronomic {  get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
