namespace E_Commerce.Domain.Entities.Basket;

public class BasketItem
{
    public int Id { get; init; } 
    public string Name { get; init; } = default!;
    public string PictureUrl { get; init; } = default!;
    public decimal Price { get; init; }
    public int Quantity { get; set; }
}
