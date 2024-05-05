using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        public OrderStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatuses()
        {
            return await _context.OrderStatuses.ToListAsync();
        }

        public async Task<OrderStatus> GetOrderStatusById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
                throw new NotFoundException($"OrderStatus with id={id} not found");

            return orderStatus;
        }

        public async Task<OrderStatus> CreateOrderStatus(OrderStatus orderStatus)
        {
            _context.OrderStatuses.Add(orderStatus);
            await _context.SaveChangesAsync();

            return orderStatus;
        }

        public async Task UpdateOrderStatus(int id, OrderStatus orderStatus)
        {
            if (id != orderStatus.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(orderStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderStatus(int id)
        {
            var orderStatus = await _context.OrderStatuses.FindAsync(id);
            if (orderStatus == null)
                throw new NotFoundException($"OrderStatus with id={id} not found");

            _context.OrderStatuses.Remove(orderStatus);
            await _context.SaveChangesAsync();
        }
    }
}
