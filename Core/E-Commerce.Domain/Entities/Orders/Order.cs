using System.Collections.ObjectModel;

namespace E_Commerce.Domain.Entities.Orders;

public class Order : Entity<Guid>
{
    public Collection<OrderItem> Items { get; set; } = []; 
    public DeliveryMethod? DeliveryMethod { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal Subtotal { get; set; }
    public string UserEmail { get; set; } = default!;
    public OrderAddress Address { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string PaymentIntentId { get; set; } = string.Empty;

}

public class OrderAddress
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
}
