using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]/{language}")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        public UsersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        //POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                string language = _mapper.GetLanguageFromHeaders(Request.Headers);
                string errorMessage = language == "ru" ?
                    "Возникла ошибка при регистрации по причине некоректных данных пользователя. Заполните необходимые поля формы регистрации и повторите попытку."
                    : "Виникла помилка під час реєстрації через неправильні дані користувача. Заповніть поля форми реєстрації та повторіть спробу.";
                return BadRequest(errorMessage);
            }

            user.CreationDate = DateTime.Now;
            user.RoleId = 2; // 1=admin, 2=user, 3=guest
            user.IsActive = false;

            var addedUser = await _userRepository.CreateUser(user);
            await _basketRepository.CreateBasketByUserId(addedUser.Id);

            return Ok();  
        }

        //POST: api/Users/ChangePassword
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO changePassword)
        {
            Request.RouteValues.TryGetValue("language", out object? lang);
            if (lang == null)
            {
                lang = "ru";
            }
            return Ok(lang);
        }
    }
}
