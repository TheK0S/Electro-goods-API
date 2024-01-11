using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using System.Security.Claims;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(User user);
        public Task<bool> ChangePassword(ChangePasswordDTO changePassword);
    }
}
