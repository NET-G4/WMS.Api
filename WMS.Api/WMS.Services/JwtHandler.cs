using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMS.Domain.Entities.Identity;
using WMS.Infrastructure.Configurations;

namespace WMS.Services;

public class JwtHandler
{
    private readonly JwtOptions _options;
    
    public JwtHandler(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string GenerateToken(User user, IList<string> roles)
    {
        var claims = GetClaims(user, roles);

        var signingKey = GetSigningKey();
        var securityToken = new JwtSecurityToken(
            issuer: _options.ValidIssuer,
            audience: _options.ValidAudience,
            claims: claims,
            expires: _options.Expires,
            signingCredentials: signingKey);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

    private SigningCredentials GetSigningKey()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
        var signingKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return signingKey;
    }

    private static List<Claim> GetClaims(User user, IList<string> roles)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Email!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}
