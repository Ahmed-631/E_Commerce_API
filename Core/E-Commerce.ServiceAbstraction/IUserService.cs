using E_Commerce.ServiceAbstraction.Common;
using E_Commerce.Shared.DataTransferObjects.Auth;
using E_Commerce.Shared.DataTransferObjects.Users;

namespace E_Commerce.ServiceAbstraction;

public interface IUserService
{
    Task<Result<UserResponse>> GetByEmailAsync(string email);
    Task<Result<AddressDTO>> GetAddressAsync(string email);
    Task<Result<AddressDTO>> UpdateAddressAsync(string email, AddressDTO address);
}
