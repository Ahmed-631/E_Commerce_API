namespace E_Commerce.Domain.Entities.Products;

public class ProductType : Entity<int>
{
    public string Name { get; set; } = default!;
}
