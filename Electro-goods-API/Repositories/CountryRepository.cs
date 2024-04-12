using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class CountryRepository : ICountryRepositiry
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountryById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var country = await _context.Countries.FindAsync(id);

            if (country == null)
                throw new NotFoundException($"Country with id={id} not found");

            return country;
        }

        public async Task UpdateCountry(int id, Country country)
        {
            if (id != country.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(country).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Country> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
                throw new NotFoundException($"Country with id={id} not found");

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }
    }
}

