using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Shared.DataTransferObjects.Auth;

public record RegisterRequest([EmailAddress] string Email, string DisplayName, string Password,
    string? UserName = "MMM", string? PhoneNumber = "" );
