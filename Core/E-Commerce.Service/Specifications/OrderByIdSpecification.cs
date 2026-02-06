using E_Commerce.Domain.Entities.Orders;
using System.Linq.Expressions;

namespace E_Commerce.Service.Specifications;

public class OrderByIdSpecification
    : BaseSpecifications<Order>
{
    public OrderByIdSpecification(Guid id, string email)
        : base(o => o.Id == id && o.UserEmail == email)
    {
        AddInclude(o => o.Items);
        AddInclude(o => o.DeliveryMethod!);
    }
}
