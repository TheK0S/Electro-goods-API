using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Electro_goods_API.Models.DTO;
using System.Data;
using Electro_goods_API.Services.Interfaces;

namespace Electro_goods_API.Controllers
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
        public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
        {
            return Ok(await _service.GetAllRoles());
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            return Ok(await _service.GetRoleById(id));
        }

        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, RoleDto role)
        {
            await _service.UpdateRole(id, role);
            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>> PostRole(RoleDto role)
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
