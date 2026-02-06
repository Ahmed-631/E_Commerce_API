using E_Commerce.ServiceAbstraction;
using E_Commerce.Shared.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Presentation.API.Controller;

public class AuthController(IAuthService service)
    : APIBaseController
{
    [HttpPost("Register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
    {
        var result = await service.RegisterAsync(request);
        return HandleResult(result);
    }
    [HttpPost("Login")]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request)
    {
        var result = await service.LoginAsync(request);
        return HandleResult(result);
    }
    [HttpPost("CheckEmail")]
    public async Task<ActionResult<UserResponse>> CheckEmail(string email) 
        => Ok(await service.CheckEmailAsync(email));
}
