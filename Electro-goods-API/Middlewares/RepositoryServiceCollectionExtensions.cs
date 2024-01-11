using Electro_goods_API.Repositories;
using Electro_goods_API.Repositories.Interfaces;

namespace Electro_goods_API.Middlewares
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<IRoleReposirory, RoleRepository>();
            services.AddTransient<ICountryRepositiry, CountryRepository>();
            services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderStatusRepository, OrderStatusRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
