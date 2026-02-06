using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistance.Context.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {

        builder.Property(x => x.Price)
            .HasColumnType("decimal(10.2)");

        builder.Property(x => x.Name)
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.PictureUrl)
            .HasColumnType("varchar")
            .HasMaxLength(128);
    }
}
