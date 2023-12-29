using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IOrderStatusRepository
    {
        Task<List<OrderStatusDto>> GetAllOrderStatuses();
        Task<OrderStatusDto> GetOrderStatusById(int id);
        Task<OrderStatusDto> CreateOrderStatus(OrderStatusDto orderStatus);
        Task UpdateOrderStatus(int id, OrderStatusDto orderStatus);
        Task DeleteOrderStatus(int id);
    }
}
