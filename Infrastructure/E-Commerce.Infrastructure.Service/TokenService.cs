using E_Commerce.Domain.Entities.Auth;
using E_Commerce.Service.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Infrastructure.Service;

public class TokenService(IOptions<JWTOptions> options)
    : ITokenService
{
    public string GetToken(ApplicationUser user, IList<string> roles)
    {
        var jwt = options.Value;

        List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Name, user.DisplayName),
                new(JwtRegisteredClaimNames.Email, user.Email)
            ];

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            expires: DateTime.Now.AddHours(jwt.DurationInHours),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
