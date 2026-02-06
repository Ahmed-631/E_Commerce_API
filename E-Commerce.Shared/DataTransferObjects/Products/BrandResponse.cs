namespace E_Commerce.Shared.DataTransferObjects.Products;

public record BrandResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}