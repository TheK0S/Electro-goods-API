using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
		private readonly AppDbContext _context;
		public BasketRepository(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}
        public async Task<Basket> CreateBasketByUserId(int userId)
        {
            _context.Baskets.Add(new Basket { UserId = userId });
            await _context.SaveChangesAsync();

            return await _context.Baskets.FirstAsync(b => b.UserId == userId);
        }
    }
}
