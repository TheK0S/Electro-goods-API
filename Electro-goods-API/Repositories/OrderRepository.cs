using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _context.Orders.ToListAsync();
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

        public async Task<Order> GetOrderById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            try
            {
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                    throw new InvalidOperationException("Order not found");

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Order>> GetOrdersByUserId(int id)
        {
            try
            {
                return await _context.Orders.Where(o => o.UserId == id).ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<Order> CreateOrder(Order order)
        {
            if (_context.Orders == null)
            {
                throw new InvalidOperationException("Orders table not found");
            }

            _context.Orders.Add(order);

            try
            {
                await _context.SaveChangesAsync();
                return order;
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

        public async Task UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!OrderExists(id))
                    throw new InvalidOperationException("Order not found");

                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                throw new InvalidOperationException("Orders table not found");
            }
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            _context.Orders.Remove(order);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool OrderExists(int id)
        {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
