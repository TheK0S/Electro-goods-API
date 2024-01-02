using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Electro_goods_API.Services.Interfaces;
using Electro_goods_API.Models.Entities;

namespace Electro_goods_API.Controllers.StoreAdmin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleReposirory _service;

        public RolesController(IRoleReposirory service)
        {
            _service = service;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetAllRoles()
        {
            return Ok(await _service.GetAllRoles());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            return Ok(await _service.GetRoleById(id));
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            await _service.UpdateRole(id, role);
            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            role = await _service.CreateRole(role);

            return CreatedAtAction("GetRoleById", new { id = role.Id }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _service.DeleteRole(id);
            return NoContent();
        }
    }
}
