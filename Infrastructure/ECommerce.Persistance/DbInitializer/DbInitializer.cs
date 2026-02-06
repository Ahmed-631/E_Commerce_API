using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using ECommerce.Persistance.AuthContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace E_Commerce.Persistance.DbInitializer;

public class DbInitializer(StoreDbContext appDbContext,
    AuthDbContext authDbContext,
    RoleManager<IdentityRole> roleManager,
    UserManager<ApplicationUser> userManager,
    ILogger<DbInitializer> logger)
    : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            //if ((await appDbContext.Database.GetAppliedMigrationsAsync()).Any())
            await appDbContext.Database.MigrateAsync();// will do everything
            // Add Brands
            if (!appDbContext.ProductBrands.Any())
            {
                var brandData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands is not null && brands.Any())
                {
                    await appDbContext.ProductBrands.AddRangeAsync(brands);
                    await appDbContext.SaveChangesAsync();
                }
            }
            // Add Product Types
            if (!appDbContext.ProductTypes.Any())
            {
                var typesData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types is not null && types.Any())
                {
                    await appDbContext.ProductTypes.AddRangeAsync(types);
                    await appDbContext.SaveChangesAsync();
                }
            }
            // Add Products
            if (!appDbContext.Products.Any())
            {
                var productsData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Any())
                {
                    await appDbContext.Products.AddRangeAsync(products);
                    await appDbContext.SaveChangesAsync();
                }
            }
            // Add DeliveryMethods
            if (!appDbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllBytesAsync(@"..\Infrastructure\ECommerce.Persistance\Context\DataSeed\delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if (deliveryMethods is not null && deliveryMethods.Count != 0)
                {
                    await appDbContext.DeliveryMethods.AddRangeAsync(deliveryMethods);
                    await appDbContext.SaveChangesAsync();
                }
            }


        }
        catch (Exception ex) 
        {
            Console.WriteLine(ex);
        }




        
    }

    public async Task InitializeAuthDbAsync()
    {
        await authDbContext.Database.MigrateAsync();

        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }

        if (!userManager.Users.Any())
        {
            var superAdminUser = new ApplicationUser
            {
                DisplayName = "Super Admin",
                Email = "SuperAdmin@gmail.com",
                UserName = "SuperAdmin",
                PhoneNumber = "01145132000"
            };
            var adminUser = new ApplicationUser
            {
                DisplayName = "Admin",
                Email = "Admin@gmail.com",
                UserName = "Admin",
                PhoneNumber = "01140002000"
            };

            await userManager.CreateAsync(superAdminUser, "passw0rd");
            await userManager.CreateAsync(adminUser, "passw0rd");
            
            await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
