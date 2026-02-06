namespace E_Commerce.Shared.DataTransferObjects.Orders;

public class DeliveryMethodResponse
{
    public string ShortName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Price { get; set; }
}
