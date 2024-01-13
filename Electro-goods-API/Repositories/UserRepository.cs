using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Electro_goods_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new UserNotFoundException($"User with Id = {id} not found");
            return user;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Role)
                    .FirstAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
        public async Task<User> CreateUser(User user)
        {
            if (_context.Users == null)
                throw new NotFoundException();

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
                return user;
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
        public async Task UpdateUser(int id,User user)
        {
            if (id != user.Id)
                throw new ArgumentException("Wrong Id");

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
    }
}
