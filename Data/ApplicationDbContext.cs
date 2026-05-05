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

        modelBuilder.Entity<ItemPrice>()
            .HasOne(ip => ip.Item)
            .WithMany(i => i.ItemPrices)
            .HasForeignKey(ip => ip.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemPrice>()
            .HasOne(ip => ip.Supplier)
            .WithMany(s => s.ItemPrices)
            .HasForeignKey(ip => ip.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}