using Electro_goods_API.Mapping;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Middlewares;
using Electro_goods_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Electro_goods_API.Models.DTO;
using System.Security.Claims;
using System.Configuration;

namespace Electro_goods_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true
                    };
                });
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddLogging();
            builder.Services.AddSingleton<IMapper, Mapper>();
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
            builder.Services.AddRepository();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.MapGet("/security/getMessage", [Authorize] (HttpContext context) =>
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");


                // Декодируем токен
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;


                // Извлекаем информацию о сроке жизни токена
                var expirationTime = jsonToken.ValidTo;
                return $"Role: {context.User.FindFirstValue(ClaimTypes.Role)}\n" +
                    $"Id: {context.User.FindFirstValue(ClaimTypes.NameIdentifier)}\n" +
                    $"Email: {context.User.FindFirstValue(ClaimTypes.Email)}\n" +
                    $"Expires: {expirationTime}\n" +
                    $"DateTime: {DateTime.Now} \n";
            });
        
                

            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}