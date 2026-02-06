namespace E_Commerce.Domain.Entities.Orders;

public class OrderItem : Entity<Guid>
{
    public int ProductId { get; set; }
    public string Name { get; init; } = default!;
    public string PictureUrl { get; init; } = default!;
    public decimal Price { get; init; }
    public int Quantity { get; set; }

    public Guid OrderId { get; init; }
}
