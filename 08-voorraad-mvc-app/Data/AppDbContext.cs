using Microsoft.EntityFrameworkCore;
using VoorraadApp.Models;
namespace VoorraadApp.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Elektronica" },
            new Category { Id = 2, Name = "Kantoor" },
            new Category { Id = 3, Name = "Gereedschap" }
        );
        modelBuilder.Entity<Supplier>().HasData(
            new Supplier { Id = 1, Name = "Tech Suppliers BV", Email = "info@tech.be", Phone = "02/123 45 67", Address = "Brussel" },
            new Supplier { Id = 2, Name = "Office World", Email = "info@office.be", Phone = "03/987 65 43", Address = "Antwerpen" }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop Pro", SKU = "LAP001", Price = 999.99m, StockQuantity = 15, SupplierId = 1, CategoryId = 1 },
            new Product { Id = 2, Name = "Muis Draadloos", SKU = "MUI001", Price = 29.99m, StockQuantity = 8, SupplierId = 1, CategoryId = 1 },
            new Product { Id = 3, Name = "Bureaulamp", SKU = "LAM001", Price = 49.99m, StockQuantity = 3, SupplierId = 2, CategoryId = 2 },
            new Product { Id = 4, Name = "Nietmachine", SKU = "NIE001", Price = 12.50m, StockQuantity = 25, SupplierId = 2, CategoryId = 2 }
        );
    }
}
