using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IOrderStatusRepository
    {
        Task<List<OrderStatus>> GetAllOrderStatuses();
        Task<OrderStatus> GetOrderStatusById(int id);
        Task<OrderStatus> CreateOrderStatus(OrderStatus orderStatus);
        Task UpdateOrderStatus(int id, OrderStatus orderStatus);
        Task DeleteOrderStatus(int id);
    }
}
