using Electro_goods_API.Exceptions;
using Electro_goods_API.Helpers;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationRepository _authRepository;
        private readonly IBasketRepository _basketRepository;

        public AuthenticationController(IUserRepository userRepository, IAuthenticationRepository authRepository, IBasketRepository basketRepository)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _basketRepository = basketRepository;
        }

        //POST: api/Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null)
                throw new UserNotFoundException("User not found");

            string requestHeshedPassword = HashPasswordHelper.HashPasword(loginRequest.Password);
            if (requestHeshedPassword != user.Password)
                throw new Exception("Password is not correct");

            if (user.Role == null)
                throw new Exception("Role is null");

            string jwtToken = _authRepository.GenerateJwtToken(user.Id, user.Email, user.Role.Name);

            return Ok(jwtToken);
        }
        //POST: api/Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerRequest)
        {
            if (registerRequest.Password != registerRequest.ConfirmPassword)
                throw new ArgumentException("Пароли не равны");

            User newUser = new()
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Patronomic = registerRequest.Patronomic,
                Email = registerRequest.Email,
                Password = HashPasswordHelper.HashPasword(registerRequest.Password),
                PhoneNumber = registerRequest.PhoneNumber,
                Birthdate = registerRequest.Birthdate ?? DateTime.Now.Date,
                CreationDate = DateTime.Now,
                IsActive = true,
                RoleId = 2,                
            };

            User registeredUser = await _userRepository.CreateUser(newUser);
            await _basketRepository.CreateBasketByUserId(registeredUser.Id);

            return Ok();
        }

        //DELETE: api/Auth/Logout
        [HttpDelete]
        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }       
    }
}
