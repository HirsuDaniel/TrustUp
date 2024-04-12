// Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserServices _userService;

    public UsersController(UserServices userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Users user)
    {
        var result = await _userService.RegisterUserAsync(user);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Users user)
    {
        var token = await _userService.AuthenticateUserAsync(user.Email, user.Password);
        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token });
    }
}