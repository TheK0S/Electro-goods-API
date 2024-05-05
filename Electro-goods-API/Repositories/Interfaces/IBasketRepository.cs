using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket> CreateBasketByUserId(int userId);
    }
}
