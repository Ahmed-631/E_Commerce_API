using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Service.Specifications;
using E_Commerce.Shared.DataTransferObjects.Orders;
using System.Collections.ObjectModel;

namespace E_Commerce.Service.Services;

public class OrderService(IUnitOfWork unitOfWork,
    IMapper mapper,
    IBasketRepository basketRepository)
    : IOrderService
{
    public async Task<Result<OrderResponse>> CreateAsync(OrderRequest request, string email, CancellationToken cancellationToken = default)
    {
        var basket = await basketRepository.GetAsync(request.BasketId);
        if (basket is null)
            return Error.NotFound("Basket Not Found",
                $"Basket With Id {request.BasketId} Was Not Found!");

        var delivery = await unitOfWork.GetRepository<DeliveryMethod>()
            .GetByIdAsync(request.DeliveryMethodId);
        if (delivery is null)
            return Error.NotFound("Delivery Method Not Found",
                $"Delivery Method With Id {request.DeliveryMethodId} Was Not Found!");

        var productRepo = unitOfWork.GetRepository<Product>();
        var ids = basket.Items.Select(i => i.Id).ToList();
        var products = (await productRepo.GetAllAsync(new GetProductByIdsSpecification(ids)))
            .ToDictionary(p => p.Id);

        var orderItems = new Collection<OrderItem>();

        var validationErrors = new List<Error>();

        foreach (var item in basket.Items)
        {
            if (!products.TryGetValue(item.Id, out Product? product))
            {
                validationErrors.Add(Error.Validation("Product Not Found",
                    $"Product With Id {item.Id} was Not Found"));
                continue;
            }

            orderItems.Add(new OrderItem
            {
                Price = product.Price,
                Quantity = item.Quantity,
                Name = item.Name,
                PictureUrl = item.PictureUrl,
                ProductId = product.Id
            });
        }

        if (validationErrors.Any())
            return validationErrors;

        var subtotal = orderItems.Sum(o => o.Quantity * o.Price);

        var address = mapper.Map<OrderAddress>(request.Address);

        var order = new Order
        {
            UserEmail = email,
            DeliveryMethod = delivery,
            Items = orderItems,
            Subtotal = subtotal,
            Address = address
        };

        unitOfWork.GetRepository<Order, Guid>().Add(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<OrderResponse>(order);
    }

    public async Task<Result<OrderResponse>> GetByIdAsync(Guid id, string email,  CancellationToken cancellationToken = default)
    {
        var result = await unitOfWork.GetRepository<Order, Guid>()
            .GetAsync(new OrderByIdSpecification(id, email));
        if (result is null)
            return Error.NotFound("Order Nor Found!");
        return mapper.Map<OrderResponse>(result);
    }

    public async Task<IEnumerable<OrderResponse>> GetByUserEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var result = await unitOfWork.GetRepository<Order, Guid>()
            .GetAllAsync(new OrdersByEmailSpecifications(email));
        return mapper.Map<IEnumerable<OrderResponse>>(result);
    }

    public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync(CancellationToken cancellationToken = default)
    {
        var result = await unitOfWork.GetRepository<DeliveryMethod>()
            .GetAllAsync();
        return mapper.Map<IEnumerable<DeliveryMethodResponse>>(result);
    }
}
