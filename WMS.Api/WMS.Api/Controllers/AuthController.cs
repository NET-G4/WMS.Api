using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WMS.Domain.Entities.Identity;
using WMS.Services;
using WMS.Services.DTOs.User;

namespace WMS.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly JwtHandler _jwtHandler;

    public AuthController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHander)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtHandler = jwtHander;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUser)
    {
        var user = _mapper.Map<User>(registerUser);

        await _userManager.CreateAsync(user, registerUser.Password);
        await _userManager.AddToRoleAsync(user, "Visitor");

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto loginUser)
    {
        var user = await _userManager.FindByNameAsync(loginUser.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, loginUser.Password))
        {
            return Unauthorized("Invalid email or password.");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtHandler.GenerateToken(user, roles);

        return Ok(token);
    }
}
