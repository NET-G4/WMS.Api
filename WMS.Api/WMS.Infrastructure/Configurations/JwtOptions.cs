using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS.Infrastructure.Configurations;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required(ErrorMessage = "Secret Key is required.")]
    [MinLength(16, ErrorMessage = "Secret Key must have more than 16 characters.")]
    public string SecretKey { get; init; }

    [Required(ErrorMessage = "Valid Audience is required.")]
    public string ValidAudience { get; init; }

    [Required(ErrorMessage = "Valid Issuer is required.")]
    public string ValidIssuer { get; init; }

    [Required(ErrorMessage = "Expires In is required.")]
    public int ExpiresInMinutes { get; init; }

    [NotMapped]
    public DateTime Expires => DateTime.UtcNow.AddMinutes(ExpiresInMinutes);
}
