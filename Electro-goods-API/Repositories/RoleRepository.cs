using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class RoleRepository : IRoleReposirory
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                throw new NotFoundException($"Role with id={id} not found");

            return role;
        }

        public async Task UpdateRole(int id, Role role)
        {
            if (id != role.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(role).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Role> CreateRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            
            return role;
        }

        public async Task DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);            
            if (role == null)
                throw new NotFoundException($"Role with id={id} not found");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}
