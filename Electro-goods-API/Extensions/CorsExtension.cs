namespace Electro_goods_API.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsExtension(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost3000", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                options.AddPolicy("AllowFrontHttps", builder =>
                {
                    builder.WithOrigins("https://r.example111.s-host.net")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                options.AddPolicy("AllowFrontHttp", builder =>
                {
                    builder.WithOrigins("http://r.example111.s-host.net")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            return services;
        }
    }
}
