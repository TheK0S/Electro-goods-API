using Electro_goods_API.Mapping;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Middlewares;
using Electro_goods_API.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Electro_goods_API.Extensions;
using Electro_goods_API.ActionFilters;

namespace Electro_goods_API
{
    public class Program        
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthorization();
            builder.Services.AddAuthenticationJwtBearer(builder);// Extensions/JwtBearerExtension.cs
            builder.Services.AddCorsExtension(builder);// Extensions/CorsExtinsion.cs
            builder.Services.AddControllers(options =>{
                options.Filters.Add(new ValidateModelAttribute());});
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local_1")));
            builder.Services.AddLogging();
            builder.Services.AddSingleton<IMapper, Mapper>();
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
            builder.Services.AddRepository();// Extensions/RepositoryServiceCollectionExtensions.cs
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseCors("AllowLocalhost3000");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

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

            app.MapControllers();

            app.Run();
        }
    }
}