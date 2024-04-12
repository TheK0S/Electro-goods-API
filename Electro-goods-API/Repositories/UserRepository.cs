using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
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
            return await _context.Users
                    .Include(u => u.Role)
                    .FirstAsync(x => x.Email == email);
        }
        public async Task<User> CreateUser(User user)
        {
            if (_context.Users == null)
                throw new NotFoundException();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task UpdateUser(int id,User user)
        {
            if (id != user.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
