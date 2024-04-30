namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IAuthenticationRepository
    {
        string GenerateJwtToken(int userId, string email, string role);
    }
}
