using Electro_goods_API.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Electro_goods_API.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IConfiguration _configuration;
        public AuthenticationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(int userId, string email, string role)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.Now.AddMinutes(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            Console.WriteLine($"expires: {tokenDescriptor.Expires}");
            Console.WriteLine($"now: {DateTime.Now}");

            return jwtToken;
        }
    }
}
