using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Shared.DataTransferObjects.Orders;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.Service.MappingProfiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.DeliveryMethod,
            o => o.MapFrom(s => s.DeliveryMethod!.ShortName))
            .ForMember(d => d.DeliveryMethodCost,
            o => o.MapFrom(s => s.DeliveryMethod!.Price))
            .ForMember(d => d.Total,
            o => o.MapFrom(s => s.DeliveryMethod!.Price + s.Subtotal));

        CreateMap<OrderAddress, AddressDTO>()
            .ReverseMap();

        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(d => d.PictureUrl,
            o => o.MapFrom<OrderPictureUrlResolver>());

        CreateMap<DeliveryMethod, DeliveryMethodResponse>();
    }
}

internal class OrderPictureUrlResolver(IConfiguration configuration)
    : IValueResolver<OrderItem, OrderItemDTO, string>
{
    public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source.PictureUrl))
            return string.Empty;
        return $"{configuration["BaseUrl"]}{source.PictureUrl}";
    }
}