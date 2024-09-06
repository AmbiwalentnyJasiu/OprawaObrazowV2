using Microsoft.AspNetCore.Mvc;
using OprawaObrazowWebApi.Resources;
using OprawaObrazowWebApi.Services.Interfaces;

namespace OprawaObrazowWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
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
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(string accessToken, string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            var response = await userService.RefreshTokens(accessToken, refreshToken, cancellationToken);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(new { ErrorMessage = e.Message });
        }
    }
}