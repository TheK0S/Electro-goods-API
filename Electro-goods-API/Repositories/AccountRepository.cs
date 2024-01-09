using Electro_goods_API.Models.DTO;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Electro_goods_API.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Task<ClaimsIdentity> Login(AuthenticateRequestDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> Register(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ClaimsIdentity> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            throw new NotImplementedException();
        }
    }
}
