using System.ComponentModel.DataAnnotations;

namespace WMS.Services.DTOs.User;

public class RegisterUserDto
{
    [Required(ErrorMessage = "First name is required.")]
    public string FirstName { get; set; }

    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
    public string ConfirmPassword { get; set; }

    public string ClientUri { get; set; }
}
