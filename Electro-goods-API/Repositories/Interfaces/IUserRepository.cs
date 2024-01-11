using Electro_goods_API.Models.DTO;
using System.Security.Claims;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<ClaimsIdentity> Authenticate(AuthenticateRequestDTO request);
        public Task<ClaimsIdentity> Register(RegisterDTO register);
        public Task<ClaimsIdentity> ChangePassword(ChangePasswordDTO changePassword);
    }
}
