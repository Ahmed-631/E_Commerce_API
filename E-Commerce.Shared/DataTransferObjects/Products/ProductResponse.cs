namespace E_Commerce.Shared.DataTransferObjects.Products;

public record ProductResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string PictureUrl { get; init; } = default!;
    public decimal Price { get; init; }
    public string Brand { get; init; } = default!;
    public string Type { get; init; } = default!;

}
