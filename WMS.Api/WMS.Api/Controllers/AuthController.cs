using AutoMapper;
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WMS.Domain.Entities.Identity;
using WMS.Infrastructure.Email;
using WMS.Infrastructure.Email.Models;
using WMS.Services;
using WMS.Services.DTOs.User;
using WMS.Services.Interfaces;

namespace WMS.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUser)
    {
        var result = await _authService.RegisterAsync(registerUser);

        if (result.Errors.Any())
        {
            return BadRequest("Invalid registertration request.");
        }

        return Accepted();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUser)
    {
        var token = await _authService.LoginAsync(loginUser);

        return Ok(token);
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPassword)
    {
        await _authService.ForgotPasswordAsync(forgotPassword);

        return NoContent();
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
    {
        var result = await _authService.ResetPasswordAsync(resetPassword);

        if (result.Errors.Any())
        {
            var errors = result.Errors.Select(x => x.Description);
            return BadRequest(errors);
        }

        return NoContent();
    }

    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
    {
        var result = await _authService.ConfirmEmailAsync(confirmEmail);

        if (result.Errors.Any())
        {
            var errors = result.Errors.Select(x => x.Description);
            return BadRequest(errors);
        }

        return NoContent();
    }
}
