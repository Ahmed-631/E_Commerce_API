namespace E_Commerce.Domain.Entities.Auth;

public class Address
{
    public ApplicationUser User { get; set; } = default!;
    public string UserId { get; set; } = default!;

    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
}
