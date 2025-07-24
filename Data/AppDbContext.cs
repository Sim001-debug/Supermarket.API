using Microsoft.EntityFrameworkCore;
using Supermarket.API.Models;


namespace Supermarket.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
