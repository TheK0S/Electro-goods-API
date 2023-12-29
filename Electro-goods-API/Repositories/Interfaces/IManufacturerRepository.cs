using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface IManufacturerRepository
    {
        Task<List<ManufacturerDto>> GetAllManufacturers();
        Task<ManufacturerDto> GetManufacturerById(int id);
        Task<ManufacturerDto> CreateManufacturer(ManufacturerDto manufacturer);
        Task UpdateManufacturer(int id, ManufacturerDto manufacturer);
        Task DeleteManufacturer(int id);
    }
}
