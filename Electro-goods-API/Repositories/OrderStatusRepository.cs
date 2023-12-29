﻿using Electro_goods_API.Models.DTO;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Electro_goods_API.Repositories
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderStatusRepository> _logger;
        public OrderStatusRepository(AppDbContext context, ILogger<OrderStatusRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<OrderStatusDto>> GetAllOrderStatuses()
        {
            try
            {
                return await _context.OrderStatuses.ToListAsync();
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

        public async Task<OrderStatusDto> GetOrderStatusById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("Wrong Id");

                var orderStatus = await _context.OrderStatuses.FindAsync(id);

                if (orderStatus == null)
                    throw new InvalidOperationException("OrderStatus not found");

                return orderStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<OrderStatusDto> CreateOrderStatus(OrderStatusDto orderStatus)
        {
            if (_context.OrderStatuses == null)
                throw new InvalidOperationException("OrderStatus not found");

            _context.OrderStatuses.Add(orderStatus);

            try
            {
                await _context.SaveChangesAsync();
                return orderStatus;
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

        public async Task UpdateOrderStatus(int id, OrderStatusDto orderStatus)
        {
            if (id != orderStatus.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(orderStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!OrderStatusExists(id))
                    throw new InvalidOperationException("OrderStatus not found");

                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteOrderStatus(int id)
        {
            if (_context.OrderStatuses == null)
                throw new InvalidOperationException("Countries table not found");

            var orderStatus = await _context.OrderStatuses.FindAsync(id);

            if (orderStatus == null)
                throw new InvalidOperationException("OrderStatus not found");

            _context.OrderStatuses.Remove(orderStatus);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool OrderStatusExists(int id)
        {
            return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}