using System.Security.Claims;
using backend.DTOs.UserPayment;
using backend.Services;
using backend.Services.UserPaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserPaymentController : Controller
{
    private readonly IUserPaymentService _userPaymentService;

    // GET
    public UserPaymentController(IUserPaymentService userPaymentService)
    {
        _userPaymentService = userPaymentService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetUserPaymentDto>>>> GetAllUserPayments()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _userPaymentService.GetAllUserPayments(userId));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserPaymentDto>>> GetUserPaymentById(int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _userPaymentService.GetUserPaymentById(userId, id));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> UpdateUserPayment(int id, [FromBody] PostPutUserPaymentDto updatedUserPayment)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _userPaymentService.UpdateUserPayment(userId, id, updatedUserPayment));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteUserPayment(int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _userPaymentService.DeleteUserPayment(userId, id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> AddUserPayment([FromBody] PostPutUserPaymentDto newUserPayment)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _userPaymentService.AddUserPayment(userId, newUserPayment));
    }
    
}