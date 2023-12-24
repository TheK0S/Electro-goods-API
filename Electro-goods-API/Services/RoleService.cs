using Electro_goods_API.Models.DTO;
using Electro_goods_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Electro_goods_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RoleService> _logger;

        public RoleService(AppDbContext context, ILogger<RoleService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<RoleDto>> GetAll()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            try
            {
                if(id <= 0)
                    throw new ArgumentOutOfRangeException("Wrong Id");

                var role = await _context.Roles.FindAsync(id);

                if (role == null)
                    throw new InvalidOperationException("Role not found");

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    

        public async Task CreateRole(RoleDto role)
        {
            if (_context.Roles == null)
            {
                throw new Exception();
            }

            
        }

        public Task DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        
        public Task<RoleDto> UpdateRole(RoleDto role)
        {
            throw new NotImplementedException();
        }
    }
}
