using Microsoft.AspNetCore.Mvc;
using ServiceAutoApp.Services.Interfaces;
using ServiceAutoApp.ViewModels;

namespace ServiceAutoApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var customer = await authService.RegisterAsync(request);
        if (customer is null)
        {
            return Conflict(new { message = "Exista deja un cont cu acest email." });
        }

        return Ok(new
        {
            message = "Cont creat cu succes.",
            user = new
            {
                name = customer.FullName,
                email = customer.Email,
                phone = customer.Phone
            }
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var customer = await authService.LoginAsync(request);
        if (customer is null)
        {
            return Unauthorized(new { message = "Email inexistent. Inregistreaza-te mai intai." });
        }

        return Ok(new
        {
            message = "Autentificare reusita.",
            user = new
            {
                name = customer.FullName,
                email = customer.Email,
                phone = customer.Phone
            }
        });
    }
}
