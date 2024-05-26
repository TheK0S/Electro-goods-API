using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.Filters;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders(OrderFilter filter);
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrdersByUserId(int id);
        Task<Order> CreateOrder(OrderDTORequest orderRequest);
        Task UpdateOrder(int id, Order order);
        Task DeleteOrder(int id);
    }
}
