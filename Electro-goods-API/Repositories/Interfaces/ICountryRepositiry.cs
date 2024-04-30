using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Repositories.Interfaces
{
    public interface ICountryRepositiry
    {
        Task<List<Country>> GetAllCountries();
        Task<Country> GetCountryById(int id);
        Task<Country> CreateCountry(Country country);
        Task UpdateCountry(int id, Country country);
        Task DeleteCountry(int id);
    }
}
