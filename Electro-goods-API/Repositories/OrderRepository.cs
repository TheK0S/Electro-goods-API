using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;

namespace Electro_goods_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(AppDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<List<Order>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Order> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrder(int id, Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}
