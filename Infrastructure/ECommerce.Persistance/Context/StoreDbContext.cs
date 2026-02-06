using Order =  E_Commerce.Domain.Entities.Orders.Order;
using System.Reflection;
using E_Commerce.Domain.Entities.Orders;

namespace ECommerce.Persistance.Context;

public class StoreDbContext(DbContextOptions<StoreDbContext> options) 
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
        => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
