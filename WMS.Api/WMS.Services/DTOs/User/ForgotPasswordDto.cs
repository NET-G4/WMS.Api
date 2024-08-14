using System.ComponentModel.DataAnnotations;

namespace WMS.Services.DTOs.User;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string ClientUri { get; set; }

    public string Device { get; set; }

    public string OperatingSystem { get; set; }
}
