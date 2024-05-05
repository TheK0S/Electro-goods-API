using Electro_goods_API.Exceptions;
using Electro_goods_API.Models;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly AppDbContext _context;

        public ManufacturerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Manufacturer>> GetAllManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer> GetManufacturerById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Wrong Id");

            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
                throw new NotFoundException($"Manufacturer with id={id} not found");

            return manufacturer;
        }

        public async Task UpdateManufacturer(int id, Manufacturer manufacturer)
        {
            if (id != manufacturer.Id)
                throw new ArgumentOutOfRangeException("Wrong Id");

            _context.Entry(manufacturer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<Manufacturer> CreateManufacturer(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();

            return manufacturer;
        }

        public async Task DeleteManufacturer(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
                throw new NotFoundException($"Manufacturer with id={id} not found");

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }
    }
}