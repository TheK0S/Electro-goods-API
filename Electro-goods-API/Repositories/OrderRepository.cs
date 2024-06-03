using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;
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

        public async Task<List<Order>> GetOrders(OrderFilter filter)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Select(o => new Order
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    UserName = o.UserName,
                    OrderDate = o.OrderDate,
                    Price = o.Price,
                    ShippingCity = o.ShippingCity,
                    ShippingAddress = o.ShippingAddress,
                    PaymentMethod = o.PaymentMethod,
                    OrderStatusId = o.OrderStatusId,
                    User = new User
                    {
                        Id = o.User.Id,
                        FirstName = o.User.FirstName,
                        LastName = o.User.LastName,
                        Patronomic = o.User.Patronomic
                    },
                    OrderStatus = new OrderStatus
                    {
                        Id = o.OrderStatus.Id,
                        StatusName = o.OrderStatus.StatusName,
                    },
                    OrderItems = o.OrderItems.ToList()
                })
                .ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .Select(o => new Order
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    UserName = o.UserName,
                    OrderDate = o.OrderDate,
                    Price = o.Price,
                    ShippingCity = o.ShippingCity,
                    ShippingAddress = o.ShippingAddress,
                    PaymentMethod = o.PaymentMethod,
                    OrderStatusId = o.OrderStatusId,
                    User = new User
                    {
                        Id = o.User.Id,
                        FirstName = o.User.FirstName,
                        LastName = o.User.LastName,
                        Patronomic = o.User.Patronomic
                    },
                    OrderStatus = new OrderStatus
                    {
                        Id = o.OrderStatus.Id,
                        StatusName = o.OrderStatus.StatusName,
                    },
                    OrderItems = o.OrderItems.ToList()
                })
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new NotFoundException($"Order with id={id} not found");

            return order;
        }

        public async Task<List<Order>> GetOrdersByUserId(int id)
        {
            return await _context.Orders
                .Where(o => o.UserId == id)
                .Include(o => o.OrderItems)
                .Select(o => new Order
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    UserName = o.UserName,
                    OrderDate = o.OrderDate,
                    Price = o.Price,
                    ShippingCity = o.ShippingCity,
                    ShippingAddress = o.ShippingAddress,
                    PaymentMethod = o.PaymentMethod,
                    OrderStatusId = o.OrderStatusId,
                    User = new User
                    {
                        Id = o.User.Id,
                        FirstName = o.User.FirstName,
                        LastName = o.User.LastName,
                        Patronomic = o.User.Patronomic
                    },
                    OrderStatus = new OrderStatus
                    {
                        Id = o.OrderStatus.Id,
                        StatusName = o.OrderStatus.StatusName,
                    },
                    OrderItems = o.OrderItems.ToList()
                })
                .ToListAsync();
        }

        public async Task<Order> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> CreateOrder(OrderRequestDTO orderRequest)
        {
            if(orderRequest is null || orderRequest.OrderItems is  null)
                throw new ArgumentNullException(nameof(orderRequest));

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                List<int> productIds = new List<int>();
                Dictionary<int,decimal> productQuantities = new Dictionary<int,decimal>();
                foreach (OrderItemRequestDTO orderItem in orderRequest.OrderItems)
                {
                    productIds.Add(orderItem.ProductId);
                    productQuantities[orderItem.ProductId] = orderItem.Quantity;
                }

                var productList = await _context.Products
                    .Where(product => productIds.Contains(product.Id))
                    .Select(p => new
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                        ProductNameUK = p.NameUK,
                        ProductPrice = p.Price
                    })
                    .ToListAsync();

                List<OrderItem> orderItems = new List<OrderItem>();
                decimal totalPrice = 0;

                foreach (var product in productList)
                {
                    int quantity = (int)productQuantities[product.ProductId];
                    decimal itemTotalPrice = product.ProductPrice * quantity;
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        ProductNameUK = product.ProductNameUK,
                        ProductPrice = product.ProductPrice,
                        Quantity = quantity
                    });
                    totalPrice += itemTotalPrice;
                }

                Order order = new Order
                {
                    Id = 0,
                    UserId = orderRequest.UserId,
                    UserName = orderRequest.UserName,
                    OrderDate = DateTime.Now,
                    Price = totalPrice,
                    ShippingCity = orderRequest.ShippingCity,
                    ShippingAddress = orderRequest.ShippingAddress,
                    OrderStatusId = 1,
                    OrderItems = orderItems
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
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
