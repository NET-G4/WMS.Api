using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WMS.Domain.Entities.Identity;

namespace WMS.Services;

public class JwtHandler
{
    private readonly IConfiguration _configuration;
    
    public JwtHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user, IList<string> roles)
    {
        var jwtOptions = _configuration.GetSection("Jwt");

        var secret = jwtOptions["SecretKey"];
        var audience = jwtOptions["ValidAudience"];
        var issuer = jwtOptions["ValidIssuer"];
        var expires = DateTime.Now.AddMinutes(double.Parse(jwtOptions["ExpiresInMinutes"]));

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email)
        };

        foreach(var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var signingKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            audience: audience,
            issuer: issuer,
            claims: claims,
            signingCredentials: signingKey,
            expires: expires);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}
