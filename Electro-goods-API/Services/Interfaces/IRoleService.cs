using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAll();
        Task<RoleDto> GetRoleById(int id);
        Task<RoleDto> CreateRole(RoleDto role);
        Task UpdateRole(RoleDto role);
        Task DeleteRole(int id);
    }
}
