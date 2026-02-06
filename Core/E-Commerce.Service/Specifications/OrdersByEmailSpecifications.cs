using E_Commerce.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications;

public class OrdersByEmailSpecifications
    : BaseSpecifications<Order>
{
    public OrdersByEmailSpecifications(string email)
        : base(o => o.UserEmail == email)
    {
        AddInclude(o => o.Items);
        AddInclude(o => o.DeliveryMethod!);
        AddOrderBy(o => o.OrderDate);
    }
}