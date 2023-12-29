using Electro_goods_API.Models.DTO;

namespace Electro_goods_API.Services.Interfaces
{
    public interface ICountryRepositiry
    {
        Task<List<CountryDto>> GetAllCountries();
        Task<CountryDto> GetCountryById(int id);
        Task<CountryDto> CreateCountry(CountryDto country);
        Task UpdateCountry(int id, CountryDto country);
        Task DeleteCountry(int id);
    }
}
