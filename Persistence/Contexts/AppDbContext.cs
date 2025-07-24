using Microsoft.EntityFrameworkCore;
using Supermarket.API.Models;

namespace Supermarket.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

                entity.HasMany(p => p.Products)
                      .WithOne(c => c.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade); // Ensures that deleting a category deletes its products

            });

            builder.Entity<Category>()
                  .HasData(new Category { Id = 1, Name = "Fruits" },
                           new Category { Id = 2, Name = "Vegetables" },
                           new Category { Id = 3, Name = "Dairy" });

            builder.Entity<Products>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.quantityInPackage).IsRequired();
                entity.Property(p => p.unitOfMeasurement).IsRequired();
            });
        }
    }
}
