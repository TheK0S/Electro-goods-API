using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;

namespace Electro_goods_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ChangePassword(ChangePasswordDTO changePassword)
        {
            throw new NotImplementedException();
        }        
    }
}
