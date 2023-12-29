using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Services.Interfaces
{
    public interface IRoleReposirory
    {
        Task<List<RoleDto>> GetAllRoles();
        Task<RoleDto> GetRoleById(int id);
        Task<RoleDto> CreateRole(RoleDto role);
        Task UpdateRole(int id,RoleDto role);
        Task DeleteRole(int id);
    }
}
