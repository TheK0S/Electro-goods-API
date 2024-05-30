using Electro_goods_API.Helpers;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Models.DTO;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly string language;
        public UsersController(IUserRepository userRepository, IBasketRepository basketRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _basketRepository = basketRepository;
            _mapper = mapper;
            language = HttpContext.Items["Language"]?.ToString() ?? "ru";
        }

        //POST: api/Users/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = language == "ru" ?
                    "Возникла ошибка при регистрации по причине некоректных данных пользователя. Заполните необходимые поля формы регистрации и повторите попытку."
                    : "Виникла помилка під час реєстрації через неправильні дані користувача. Заповніть поля форми реєстрації та повторіть спробу.";
                return BadRequest(errorMessage);
            }

            user.CreationDate = DateTime.Now;
            user.RoleId = 2; // 1=admin, 2=user, 3=guest
            user.IsActive = false;
            user.Password = HashPasswordHelper.HashPasword(user.Password);

            var addedUser = await _userRepository.CreateUser(user);
            await _basketRepository.CreateBasketByUserId(addedUser.Id);

            return Ok();  
        }

        //POST: api/Users/ChangePassword
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            if (!ModelState.IsValid)
            {
                string validationErrorMessage = language == "ru" ?
                    "Новый пароль не соответствует уловиям безопасности или некорректный адрес электронной почты. Измените данные и повторите попытку."
                    : "Новий пароль не відповідає вимогам безпеки або неправильним адресам електронної пошти. Змініть дані та повторіть спробу.";
                return BadRequest(validationErrorMessage);
            }

            string errorMessage = language == "ru" ?
                "Неверный адресс электронной почты или пароль"
                : "Неправильна адреса електронної пошти або пароль";

            User user = await _userRepository.GetUserByEmail(changePassword.Email);
            if (user == null)
                return BadRequest(errorMessage);

            string oldPasswordHash = HashPasswordHelper.HashPasword(changePassword.OldPassword);
            if (user.Password != oldPasswordHash)
                return BadRequest(errorMessage);

            string newPasswordHash = HashPasswordHelper.HashPasword(changePassword.NewPassword);
            user.Password = newPasswordHash;
            await _userRepository.UpdateUser(user.Id, user);
            return Ok();
        }
    }
}
