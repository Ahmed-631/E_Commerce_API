namespace E_Commerce.Domain.Contracts;

public interface IDbInitializer
{
    Task InitializeAsync();
    Task InitializeAuthDbAsync();
}
