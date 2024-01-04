using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrdersByUserId(int id);
        Task<Order> CreateOrder(Order order);
        Task UpdateOrder(int id, Order order);
        Task DeleteOrder(int id);
    }
}
