using Electro_goods_API.Models.DTO;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authRepository;

        public AuthController(IUserRepository userRepository, IAuthenticationRepository authRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
        }

        //POST: api/Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null)
                throw new Exception("User not found");
            if (user.Role == null)
                throw new Exception("Role is null");

            string jwtToken = _authRepository.GenerateJwtToken(user.Id, user.Email, user.Role.Name);

            return Ok(jwtToken);
        }

        //DELETE: api/Auth/Logout
        [HttpDelete]
        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }       
    }
}
