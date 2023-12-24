using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Models.DTO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        DbSet<UserDto> Users { get; set; }        
        DbSet<RoleDto> Roles { get; set; }
        DbSet<ProductDto> Products { get; set; }
        DbSet<CategoryDto> Categories { get; set; }
        DbSet<CountryDto> Countries { get; set; }
        DbSet<ManufacturerDto> Manufacturers { get; set; }
        DbSet<ProductAttributDto> ProductAttributs { get; set; }
        DbSet<BasketDto> Baskets { get; set; }
        DbSet<BasketItemDto> BasketItems { get; set; }
        DbSet<OrderDto> Orders { get; set; }
        DbSet<OrderItemDto> OrderItems { get; set; }
        DbSet<OrderStatusDto> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<UserDto>().HasAlternateKey(u => u.PhoneNumber);
        }
    }
}
