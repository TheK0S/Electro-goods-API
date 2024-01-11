using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class CountryRepository : ICountryRepositiry
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CountryRepository> _logger;

        public CountryRepository(AppDbContext context, ILogger<CountryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            try
            {
                return await _context.Countries.ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Country> GetCountryById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("Wrong Id");

                var country = await _context.Countries.FindAsync(id);

                if (country == null)
                    throw new InvalidOperationException("Country not found");

                return country;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                throw new ArgumentOutOfRangeException("Wrong Id");
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CountryExists(id))
                {
                    throw new InvalidOperationException("Country not found");
                }

                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<Country> CreateCountry(Country country)
        {
            if (_context.Countries == null)
            {
                throw new InvalidOperationException("Counries not found");
            }

            _context.Countries.Add(country);

            try
            {
                await _context.SaveChangesAsync();
                return country;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task DeleteCountry(int id)
        {
            if (_context.Countries == null)
            {
                throw new InvalidOperationException("Countries table not found");
            }
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                throw new InvalidOperationException("Country not found");
            }

            _context.Countries.Remove(country);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

