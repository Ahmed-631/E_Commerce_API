using E_Commerce.Domain.Entities.Basket;

namespace E_Commerce.Domain.Contracts;

public interface IBasketRepository
{
    Task<bool> DeleteAsync(string id);
    Task<CustomerBasket?> GetAsync(string id);
    Task<CustomerBasket> CreateOrUpdateAsync(CustomerBasket customerBasket, TimeSpan? TTL = null);
}
