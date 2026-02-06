using E_Commerce.Shared.DataTransferObjects.Basket;

namespace E_Commerce.ServiceAbstraction;

public interface IBasketService
{
    Task<CustomerBasketDTO> CreateOrUpdateAsync(CustomerBasketDTO customerBasket);
    Task<CustomerBasketDTO> GetByIdAsync(string id);
    Task DeleteAsync(string id);    
}
