namespace E_Commerce.Domain.Entities.Orders;

public class DeliveryMethod : Entity<int>
{
    public string ShortName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Price { get; set; }
}
