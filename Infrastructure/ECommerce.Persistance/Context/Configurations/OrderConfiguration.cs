using Order =  E_Commerce.Domain.Entities.Orders.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistance.Context.Configurations;

public class OrderConfiguration: IEntityTypeConfiguration<Order>
{

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.DeliveryMethod)
            .WithMany()
            .HasForeignKey(x => x.DeliveryMethodId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(x => x.Subtotal)
            .HasColumnType("decimal(10.2)");

        builder.Property(x => x.UserEmail)
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.Property(x => x.PaymentIntentId)
            .HasColumnType("varchar")
            .HasMaxLength(128);

        builder.HasIndex(x => x.UserEmail);

        builder.OwnsOne(x => x.Address, x => x.WithOwner());

        builder.Property(x => x.Status)
            .HasConversion<string>();
            
 
    }
}
