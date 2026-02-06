using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Auth;
using E_Commerce.Shared.DataTransferObjects.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.API.Controller;

public class UsersController(IUserService userService) 
    : APIBaseController
{
    [HttpGet]
    public async Task<ActionResult<UserResponse>> GetUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await userService.GetByEmailAsync(email!);
        return HandleResult(user);
    }
    [HttpGet("Address")]
    public async Task<ActionResult<AddressDTO>> GetAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await userService.GetAddressAsync(email!);
        return HandleResult(result);
    }
    [HttpPut("Address")]
    public async Task<ActionResult<AddressDTO>> UpdateAddress(AddressDTO address)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var result = await userService.UpdateAddressAsync(email!, address);
        return HandleResult(result);
    }

}
