using E_Commerce.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace E_Commerce.Service.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
