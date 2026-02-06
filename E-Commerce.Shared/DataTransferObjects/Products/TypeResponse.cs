namespace E_Commerce.Shared.DataTransferObjects.Products;

public record TypeResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
