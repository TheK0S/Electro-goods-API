using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesAdminController : ControllerBase
    {
        private readonly ICountryRepositiry _service;

        public CountriesAdminController(ICountryRepositiry service)
        {
            _service = service;
        }

        // GET: api/CountriesAdmin
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAllCountries()
        {
            return Ok(await _service.GetAllCountries());
        }

        // GET: api/CountriesAdmin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(int id)
        {
            return Ok(await _service.GetCountryById(id));
        }

        // PUT: api/CountriesAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            await _service.UpdateCountry(id, country);
            return NoContent();
        }

        // POST: api/CountriesAdmin
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            country = await _service.CreateCountry(country);

            return CreatedAtAction("GetCountryById", new { id = country.Id }, country);
        }

        // DELETE: api/CountriesAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _service.DeleteCountry(id);
            return NoContent();
        }
    }
}
