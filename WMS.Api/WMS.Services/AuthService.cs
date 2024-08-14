using AutoMapper;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using WMS.Domain.Entities.Identity;
using WMS.Domain.Exceptions;
using WMS.Infrastructure.Email;
using WMS.Infrastructure.Email.Factories;
using WMS.Infrastructure.Email.Models;
using WMS.Services.DTOs.User;
using WMS.Services.Interfaces;

namespace WMS.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly JwtHandler _jwtHandler;
    private readonly IEmailService _emailService;
    private readonly UserManager<User> _userManager;
    private readonly IEmailMetadaFactory _emailMetadaFactory;

    public AuthService(
        IMapper mapper, 
        JwtHandler jwtHandler, 
        IEmailService emailService,
        UserManager<User> userManager,
        IEmailMetadaFactory emailMetadaFactory)
    {
        _mapper = mapper;
        _jwtHandler = jwtHandler;
        _emailService = emailService;
        _userManager = userManager;
        _emailMetadaFactory = emailMetadaFactory;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterUserDto registerDto)
    {
        var user = _mapper.Map<User>(registerDto);
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Errors.Any())
        {
            return result;
        }

        result = await _userManager.AddToRoleAsync(user, "Admin");

        if (result.Errors.Any())
        {
            return result;
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var clientUri = GetCallbackUri(registerDto.ClientUri, token, registerDto.Email);
        var userInfo = new UserInfo(registerDto.Email, registerDto.FirstName);
        var metadata = _emailMetadaFactory.Create(
            EmailType.EmailConfirmation,
            user.Email!,
            userInfo,
            clientUri);

        await _emailService.SendAsync(metadata);

        return result;
    }

    public async Task<string> LoginAsync(LoginUserDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null || !user.EmailConfirmed)
        {
            throw new InvalidLoginAttemptException();
        }

        if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new InvalidLoginAttemptException();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtHandler.GenerateToken(user, roles);

        return token;
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

        if (user is null || !user.EmailConfirmed)
        {
            throw new InvalidUserException();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user!);
        var clientUri = GetCallbackUri(forgotPasswordDto.ClientUri, token, forgotPasswordDto.Email);
        var userInfo = new UserInfo(
            user.Email!, 
            user.FirstName, 
            forgotPasswordDto.Device,
            forgotPasswordDto.OperatingSystem);
        var metadata = _emailMetadaFactory.Create(
            EmailType.ForgotPassword, 
            user.Email!,
            userInfo, 
            clientUri);

        await _emailService.SendAsync(metadata);
    }

    public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

        if (user is null || !user.EmailConfirmed)
        {
            throw new InvalidUserException();
        }

        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

        return result;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
    {
        var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);

        if (user is null)
        {
            throw new InvalidUserException();
        }

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);

        return result;
    }

    private static string GetCallbackUri(string clientUri, string token, string email)
    {
        var param = new Dictionary<string, string?>
        {
            {"token", token },
            {"email", email }
        };
        var callbackUri = QueryHelpers.AddQueryString(clientUri, param);

        return callbackUri;
    }
}
