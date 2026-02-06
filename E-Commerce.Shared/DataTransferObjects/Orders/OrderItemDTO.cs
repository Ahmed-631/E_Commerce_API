namespace E_Commerce.Shared.DataTransferObjects.Orders;

public class OrderItemDTO
{
    public Guid OrderId { get; init; }
    public int ProductId { get; set; }
    public string Name { get; init; } = default!;
    public string PictureUrl { get; init; } = default!;
    public decimal Price { get; init; }
    public int Quantity { get; set; }
}
