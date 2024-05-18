using Microsoft.AspNetCore.Mvc;
using OprawaObrazowWebApi.Resources;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterResource resource, CancellationToken cancellationToken)
    {
        try
        {
            var response = await userService.Register(resource, cancellationToken);
            return Ok(response);
        }
        catch(Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginResource resource, CancellationToken cancellationToken)
    {
        try
        {
            var response = await userService.Login(resource, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}