using backend.DTOs.User;
using backend.Models;
using backend.Services;
using backend.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody] UserRegisterDto request)
    {
        var response = await _authService.Register(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
    
    [HttpPost("verify")]
    public async Task<ActionResult<ServiceResponse<int>>> VerifyUser(string token)
    {
        var response = await _authService.VerifyUser(token);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<string>>> Login([FromBody] UserLoginDto request)
    {
        var response = await _authService.Login(request.Email, request.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
    
    [HttpPost("google")]
    public async Task<ActionResult<ServiceResponse<string>>> AuthenticateWithGoogle([FromBody] GoogleAuthRequest request)
    {
        var response = await _authService.AuthenticateWithGoogle(request.Token);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
