namespace E_Commerce.Service.Exceptions;

public abstract class NotFoundException(string message) : Exception(message);

public sealed class ProductNotFoundException(int id) : 
    NotFoundException($"Product With Id {id} Was Not Found");

public sealed class BasketNotFoundException(string id) :  
    NotFoundException($"Basket With Id {id} Was Not Found");
