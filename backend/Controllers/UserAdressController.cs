using System.Security.Claims;
using backend.DTOs.UserAdress;
using backend.Services;
using backend.Services.UserAdressService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserAdressController : Controller
{
    private readonly IUserAdressService _adressService;

    public UserAdressController(IUserAdressService adressService)
    {
        _adressService = adressService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetUserAdressDto>>>> GetAllUserAdresses()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _adressService.GetAllUserAdresses(userId));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserAdressDto>>> GetUserAdressById(int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _adressService.GetUserAdressById(userId, id));
    }
    
    [HttpPut("{id}")]
    
    public async Task<ActionResult<ServiceResponse<int>>> UpdateUserAdress(int id,[FromBody] PutUserAdressDto updatedUserAdress)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _adressService.UpdateUserAdress(userId, id, updatedUserAdress));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteUserAdress(int id)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _adressService.DeleteUserAdress(userId, id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> AddUserAdress([FromBody] PostUserAdressDto newUserAdress)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _adressService.AddUserAdress(userId, newUserAdress));
    }
}