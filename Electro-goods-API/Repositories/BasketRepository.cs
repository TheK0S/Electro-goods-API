using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
		private readonly AppDbContext _context;
		private  readonly ILogger<BasketRepository> _logger;
		public BasketRepository(AppDbContext appDbContext, ILogger<BasketRepository> logger)
		{
			_context = appDbContext;
			_logger = logger;
		}
        public async Task<Basket> CreateBasketByUserId(int userId)
        {
            _context.Baskets.Add(new Basket { UserId = userId });
            try
			{
				await _context.SaveChangesAsync();
                return await _context.Baskets.FirstAsync(b => b.UserId == userId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, ex);
				throw;
			}
        }
    }
}
