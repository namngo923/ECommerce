using Microsoft.EntityFrameworkCore;
using ProductService.Domain;

namespace ProductService.Infrastructure;

public class ProductContext : DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    // DbSets represent tables in the database
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // You can configure additional relationships, indexes, etc. here
        // For example: 
        // modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();

        // Seed some initial data (optional)
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop", Price = 1200, Stock = 50 },
            new Product { Id = 2, Name = "Phone", Description = "A smartphone", Price = 800, Stock = 150 }
        );
    }
}

