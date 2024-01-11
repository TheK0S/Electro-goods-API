using Electro_goods_API.Models.DTO;
using Electro_goods_API.Repositories.Interfaces;
using System.Security.Claims;

namespace Electro_goods_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<ClaimsIdentity> Authenticate(AuthenticateRequestDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> Register(RegisterDTO register)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> ChangePassword(ChangePasswordDTO changePassword)
        {
            throw new NotImplementedException();
        }
    }
}
