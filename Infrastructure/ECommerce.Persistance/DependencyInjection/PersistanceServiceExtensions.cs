using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Persistance.DbInitializer;
using E_Commerce.Service.Services;
using ECommerce.Persistance.AuthContext;
using ECommerce.Persistance.Context;
using ECommerce.Persistance.Repositories;
using ECommerce.Persistance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ECommerce.Persistance.DependencyInjection;

public static class PersistanceServiceExtensions
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("SQLConnection"))
            );
        services.AddDbContext<AuthDbContext>(options 
            => options.UseSqlServer(configuration.GetConnectionString("AuthConnection"))
            );
        services.AddSingleton<IConnectionMultiplexer>(cfg => 
        {
            return ConnectionMultiplexer
            .Connect(configuration.GetConnectionString("RedisConnection")!);
        });
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICashService, CashService>();

        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddScoped<IOrderService, OrderService>();

        services.AddIdentityCore<ApplicationUser>(cfg =>
        {
            cfg.Password.RequireNonAlphanumeric = false;
            cfg.Password.RequireUppercase = false;
            cfg.Password.RequireLowercase = false;
            cfg.Password.RequireDigit = false;
            cfg.User.RequireUniqueEmail = true;
        }).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>(); 

        //services.AddScoped<IUnitOfWork, IUnitOfWork>();

        return services;
    }
}
