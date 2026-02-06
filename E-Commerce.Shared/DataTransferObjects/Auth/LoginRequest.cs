using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Shared.DataTransferObjects.Auth;

public record LoginRequest([EmailAddress] string Email, string Password);
