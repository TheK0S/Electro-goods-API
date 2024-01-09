using Electro_goods_API.Models.DTO;
using System.Security.Claims;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<ClaimsIdentity> Login(AuthenticateRequestDTO loginDTO);
        Task<ClaimsIdentity> Register(RegisterDTO registerDTO);
        Task<ClaimsIdentity> ChangePassword(ChangePasswordDTO changePasswordDTO);
    }
}
