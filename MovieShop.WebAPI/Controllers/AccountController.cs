using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
    {
        try
        {
            await _userService.RegisterUser(model);
            return Created("/api/account/login", new { message = "Registration successful. Please login." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userService.ValidateUser(model.Email, model.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        return Ok(new
        {
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            IsAdmin = user.Email.Contains("admin", StringComparison.OrdinalIgnoreCase)
        });
    }
}
