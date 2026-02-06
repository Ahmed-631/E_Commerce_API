namespace E_Commerce.ServiceAbstraction;

public interface ICashService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, object value, TimeSpan TTL);
}
