using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WMS.Domain.Entities;
using WMS.Domain.Entities.Views;

namespace WMS.Infrastructure.Persistence;

public class WmsDbContext(DbContextOptions<WmsDbContext> options) : DbContext(options)
{
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<SaleItem> SaleItems { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Supply> Supplies { get; set; }
    public virtual DbSet<SupplyItem> SupplyItems { get; set; }
    public virtual DbSet<SalesByCategory> SalesByCategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
