using E_Commerce.Domain.Entities.Basket;
using E_Commerce.Shared.DataTransferObjects.Basket;

namespace E_Commerce.Service.Services;

internal class BasketService(IBasketRepository basketRepository, IMapper mapper)
    : IBasketService
{
    public async Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO customerBasket)
    {
        var basket = mapper.Map<CustomerBasket>(customerBasket);
        var result = await basketRepository.CreateOrUpdateAsync(basket!);
        return mapper.Map<CustomerBasketDTO>(result);
    }

    public async Task DeleteAsync(string id)
    {
        await basketRepository.DeleteAsync(id);
    }

    public async Task<CustomerBasketDTO> GetByIdAsync(string id)
    {
        var basket = await basketRepository.GetAsync(id);
        return mapper.Map<CustomerBasketDTO>(basket);
    }
}
