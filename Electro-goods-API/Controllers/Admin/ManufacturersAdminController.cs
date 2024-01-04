using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersAdminController : ControllerBase
    {
        private readonly IManufacturerRepository _service;

        public ManufacturersAdminController(IManufacturerRepository service)
        {
            _service = service;
        }

        // GET: api/ManufacturersAdmin
        [HttpGet]
        public async Task<ActionResult<List<Manufacturer>>> GetAllCountries()
        {
            return Ok(await _service.GetAllManufacturers());
        }

        // GET: api/ManufacturersAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturer>> GetCountryById(int id)
        {
            return Ok(await _service.GetManufacturerById(id));
        }

        // PUT: api/ManufacturersAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Manufacturer manufacturer)
        {
            await _service.UpdateManufacturer(id, manufacturer);
            return NoContent();
        }

        // POST: api/ManufacturersAdmin
        [HttpPost]
        public async Task<ActionResult<Manufacturer>> PostCountry(Manufacturer manufacturer)
        {
            manufacturer = await _service.CreateManufacturer(manufacturer);

            return CreatedAtAction("GetCountryById", new { id = manufacturer.Id }, manufacturer);
        }

        // DELETE: api/ManufacturersAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _service.DeleteManufacturer(id);
            return NoContent();
        }
    }
}
