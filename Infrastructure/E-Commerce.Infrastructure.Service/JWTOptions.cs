namespace E_Commerce.Infrastructure.Service;

public class JWTOptions
{
    public static string SectionName { get; set; } = "JWTOptions";
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int DurationInHours { get; set; }
}
