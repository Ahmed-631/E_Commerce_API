using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistance.Context.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(x => x.Price)
            .HasColumnType("decimal(10.2)");

        builder.Property(x => x.ShortName)
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.DeliveryTime)
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.Description)
            .HasColumnType("varchar")
            .HasMaxLength(128);
    }
}
