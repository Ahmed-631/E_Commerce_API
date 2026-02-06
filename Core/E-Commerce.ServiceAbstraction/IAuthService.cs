using E_Commerce.ServiceAbstraction.Common;
using E_Commerce.Shared.DataTransferObjects.Auth;

namespace E_Commerce.ServiceAbstraction;

public interface IAuthService
{
    Task<Result<UserResponse>> LoginAsync(LoginRequest request);
    Task<Result<UserResponse>> RegisterAsync(RegisterRequest request);
    Task<bool> CheckEmailAsync(string email);
}
