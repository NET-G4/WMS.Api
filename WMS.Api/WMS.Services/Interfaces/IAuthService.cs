using Microsoft.AspNetCore.Identity;
using WMS.Services.DTOs.User;

namespace WMS.Services.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterUserDto registerDto);
    Task<string> LoginAsync(LoginUserDto loginDto);
    Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
}
