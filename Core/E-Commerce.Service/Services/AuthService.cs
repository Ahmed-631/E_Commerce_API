using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Service.Contracts;
using E_Commerce.ServiceAbstraction.Common;
using E_Commerce.Shared.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Service.Services;

public class AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    : IAuthService
{
    public async Task<bool> CheckEmailAsync(string email)
        => await userManager.FindByEmailAsync(email) != null;

    public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Error.Unauthorized(description: "Invalid email or password");
        var result = await userManager.CheckPasswordAsync(user, request.Password);
        if(!result)
            return Error.Unauthorized(description: "Invalid email or password");

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.GetToken(user, roles);

        return new UserResponse(user.Email, user.DisplayName, token);
    }

    public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            DisplayName = request.DisplayName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return result.Errors.Select(e => Error.Validation(code: e.Code, description: e.Description))
                .ToList();
        var token = tokenService.GetToken(user, []);

        return new UserResponse(user.Email, user.DisplayName, token);
    }
}
