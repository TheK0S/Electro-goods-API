using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]/{language}")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //POST: api/Users/Register
        [HttpPost("Register")]
        public Task<IActionResult> Register([FromBody] User user)
        {
            throw new NotImplementedException();
        }

        //POST: api/Users/ChangePassword
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO changePassword)
        {
            Request.RouteValues.TryGetValue("language", out object? lang);
            if (lang == null)
            {
                lang = "en";
            }
            return Ok(lang);
        }
    }
}
