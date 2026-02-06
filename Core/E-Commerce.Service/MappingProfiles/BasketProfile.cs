using E_Commerce.Domain.Entities.Basket;
using E_Commerce.Shared.DataTransferObjects.Basket;

namespace E_Commerce.Service.MappingProfiles;

internal class BasketProfile :Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, CustomerBasketDTO>()
            .ReverseMap();
        CreateMap<BasketItem, BasketItemDTO>()
            .ReverseMap();
    }
}
