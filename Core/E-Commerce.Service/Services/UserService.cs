using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service.Services;

public class UserService(UserManager<ApplicationUser> userManager,
    ITokenService tokenService,
    IMapper mapper)
    : IUserService
{
    public async Task<Result<UserResponse>> GetByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null) 
            return Error.NotFound("User not found", $"User wiht email {email} is not found!");

        var roles = await userManager.GetRolesAsync(user);

        return new UserResponse(user.Email!, user.DisplayName!, tokenService.GetToken(user, roles));
    }
    public async Task<Result<AddressDTO>> GetAddressAsync(string email)
    {
        var user = await userManager.Users
            .Include(u => u.Address)
            .FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return Error.NotFound("User not found", $"User wiht email {email} is not found!");
        if (user.Address is null)
            return Error.NotFound("Address not found", $"User wiht email {email} does not have an address!");

        return mapper.Map<AddressDTO>(user.Address);
    }


    public async Task<Result<AddressDTO>> UpdateAddressAsync(string email, AddressDTO address)
    {
        var user = await userManager.Users
    .Include(u => u.Address)
    .FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return Error.NotFound("User not found", $"User wiht email {email} is not found!");
        if (user.Address is not null)
        {
            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.Street = address.Street;
            user.Address.City = address.City;
            user.Address.Country = address.Country;
        }
        else
            user.Address = mapper.Map<Address>(address);
        

        await userManager.UpdateAsync(user);

        return mapper.Map<AddressDTO>(user.Address);
    }
}
