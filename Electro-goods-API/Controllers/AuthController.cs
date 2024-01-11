using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //POST: api/Auth/Login
        [HttpPost]
        public Task<IActionResult> Login(LoginRequestDTO login)
        {
            throw new NotImplementedException();
        }

        //DELETE: api/Auth/Logout
        [HttpDelete]
        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }       
    }
}
