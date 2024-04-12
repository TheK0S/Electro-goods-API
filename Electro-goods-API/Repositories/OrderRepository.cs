using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with id={id} not found");

            return order;
        }

        public async Task<List<Order>> GetOrdersByUserId(int id)
        {
            return await _context.Orders.Where(o => o.UserId == id).ToListAsync();
        }

        public async Task<Order> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with id={id} not found");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
