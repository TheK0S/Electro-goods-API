using Electro_goods_API.Mapping;
using Electro_goods_API.Middlewares;
using Electro_goods_API.Models;
using Electro_goods_API.Repositories;
using Electro_goods_API.Repositories.Interfaces;
using Electro_goods_API.Services;
using Electro_goods_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddLogging();
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

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}