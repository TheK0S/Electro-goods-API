using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IRoleReposirory
    {
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<Role> CreateRole(Role role);
        Task UpdateRole(int id,Role role);
        Task DeleteRole(int id);
    }
}
