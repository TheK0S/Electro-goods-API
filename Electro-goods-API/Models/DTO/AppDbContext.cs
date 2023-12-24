using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Models.DTO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<RoleDto> Roles { get; set; }
        public DbSet<ProductDto> Products { get; set; }
        public DbSet<CategoryDto> Categories { get; set; }
        public DbSet<CountryDto> Countries { get; set; }
        public DbSet<ManufacturerDto> Manufacturers { get; set; }
        public DbSet<ProductAttributDto> ProductAttributs { get; set; }
        public DbSet<BasketDto> Baskets { get; set; }
        public DbSet<BasketItemDto> BasketItems { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<OrderItemDto> OrderItems { get; set; }
        public DbSet<OrderStatusDto> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<UserDto>().HasAlternateKey(u => u.PhoneNumber);
        }
    }
}
