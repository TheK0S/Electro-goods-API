using Microsoft.EntityFrameworkCore;

namespace Electro_goods_API.Models
{
    public class AppDbContext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<User>().HasAlternateKey(u => u.PhoneNumber);
        }
    }
}
