using E_Commerce.Shared.DataTransferObjects.Users;
using System.Collections.ObjectModel;

namespace E_Commerce.Shared.DataTransferObjects.Orders;

public class OrderResponse
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = default!;
    public string? DeliveryMethod { get; set; }
    public AddressDTO Address { get; set; } = default!;
    public decimal? DeliveryMethodCost { get; set; }    
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public string Status { get; set; } = default!;
    public string PaymentIntentId { get; set; } = string.Empty;
    public Collection<OrderItemDTO> Items { get; set; } = [];
}
