using Electro_goods_API.Mapping;
using Electro_goods_API.Mapping.Interfaces;
using Electro_goods_API.Middlewares;
using Electro_goods_API.Models;
using Electro_goods_API.Repositories;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services;
using Electro_goods_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Electro_goods_API.Models.Entities;
using Electro_goods_API.Models.DTO;
using System.Security.Claims;

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
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                });
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddLogging();
            builder.Services.AddSingleton<IMapper, Mapper>();
            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
            builder.Services.AddTransient<IRoleReposirory, RoleRepository>();
            builder.Services.AddTransient<ICountryRepositiry, CountryRepository>();
            builder.Services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            builder.Services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.MapGet("/security/getMessage", () => "Hello World!").RequireAuthorization();
            app.MapPost("/security/createToken",
            [AllowAnonymous] (AuthenticateRequestDTO user) =>
            {
                if (user.Email == "TheKos" && user.Password == "TheKos123")
                {
                    var issuer = builder.Configuration["Jwt:Issuer"];
                    var audience = builder.Configuration["Jwt:Audience"];
                    var key = Encoding.ASCII.GetBytes
                    (builder.Configuration["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
                         }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);
                    return Results.Ok(stringToken);
                }
                return Results.Unauthorized();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}