using Microsoft.EntityFrameworkCore;
using price_management_api.Entities;

namespace price_management_api.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<ItemPrice> ItemPrices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Item>().HasIndex(i => i.ItemCode).IsUnique();
        modelBuilder.Entity<Supplier>().HasIndex(s => s.SupplierCode).IsUnique();
    }
}