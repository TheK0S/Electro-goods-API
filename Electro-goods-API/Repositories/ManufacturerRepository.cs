using Electro_goods_API.Models.DTO;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ManufacturerRepository> _logger;

        public ManufacturerRepository(AppDbContext context, ILogger<ManufacturerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ManufacturerDto>> GetAllManufacturers()
        {
            try
            {
                return await _context.Manufacturers.ToListAsync();
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

        public async Task<ManufacturerDto> GetManufacturerById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentOutOfRangeException("Wrong Id");

                var manufacturer = await _context.Manufacturers.FindAsync(id);

                if (manufacturer == null)
                    throw new InvalidOperationException("Manufacturer not found");

                return manufacturer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateManufacturer(int id, ManufacturerDto manufacturer)
        {
            if (id != manufacturer.Id)
            {
                throw new ArgumentOutOfRangeException("Wrong Id");
            }

            _context.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ManufacturerExists(id))
                {
                    throw new InvalidOperationException("Manufacturer not found");
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


        public async Task<ManufacturerDto> CreateManufacturer(ManufacturerDto manufacturer)
        {
            if (_context.Manufacturers == null)
            {
                throw new InvalidOperationException("Manufacturers table not found");
            }

            _context.Manufacturers.Add(manufacturer);

            try
            {
                await _context.SaveChangesAsync();
                return manufacturer;
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

        public async Task DeleteManufacturer(int id)
        {
            if (_context.Manufacturers == null)
            {
                throw new InvalidOperationException("Manufacturers table not found");
            }
            var manufacturer = await _context.Manufacturers.FindAsync(id);

            if (manufacturer == null)
            {
                throw new InvalidOperationException("Manufacturer not found");
            }

            _context.Manufacturers.Remove(manufacturer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool ManufacturerExists(int id)
        {
            return (_context.Manufacturers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}