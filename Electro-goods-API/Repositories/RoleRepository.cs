using Electro_goods_API.Models.DTO;
using Electro_goods_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Electro_goods_API.Services
{
    public class RoleRepository : IRoleReposirory
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(AppDbContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<RoleDto>> GetAllRoles()
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
                    throw new InvalidOperationException("Not found");

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateRole(int id, RoleDto role)
        {
            if (id != role.Id)
            {
                throw new ArgumentOutOfRangeException("Wrong Id");
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RoleExists(id))
                {
                    throw new InvalidOperationException("Not found");
                }

                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<RoleDto> CreateRole(RoleDto role)
        {
            if (_context.Roles == null)
            {
                throw new InvalidOperationException("Not found");
            }

            _context.Roles.Add(role);

            try
            {
                await _context.SaveChangesAsync();
                return role;
            }
            catch (DbUpdateConcurrencyException ex)
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

        public async Task DeleteRole(int id)
        {
            if (_context.Roles == null)
                throw new InvalidOperationException("Not found");

            var role = await _context.Roles.FindAsync(id);
            
            if (role == null)
                throw new InvalidOperationException("Not found");

            _context.Roles.Remove(role);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool RoleExists(int id)
        {
            return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
