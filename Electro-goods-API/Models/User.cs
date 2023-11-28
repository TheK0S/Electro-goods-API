namespace Electro_goods_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronomic { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateTime CreationDate { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
