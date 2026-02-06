using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistance.Context.Configurations;

internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(p => p.Name)
            .HasColumnType("VarChar")
            .HasMaxLength(256);
    }
}
