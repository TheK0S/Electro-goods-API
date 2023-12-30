using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<List<Manufacturer>> GetAllManufacturers();
        Task<Manufacturer> GetManufacturerById(int id);
        Task<Manufacturer> CreateManufacturer(Manufacturer manufacturer);
        Task UpdateManufacturer(int id, Manufacturer manufacturer);
        Task DeleteManufacturer(int id);
    }
}
