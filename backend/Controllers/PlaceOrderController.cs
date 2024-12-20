using System.Security.Claims;
using backend.DTOs.Order;
using backend.DTOs.UserAdress;
using backend.Services;
using backend.Services.PlaceOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlaceOrderController : Controller
{
    private readonly IPlaceOrderService _placeOrderService;

    public PlaceOrderController(IPlaceOrderService placeOrderService)
    {
        _placeOrderService = placeOrderService;
    }
    
    //POST place order
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> PlaceOrder([FromBody] PlaceOrderDto order)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _placeOrderService.PlaceOrder(userId, order));
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetOrdersDto>>>> GetOrdersByUser()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _placeOrderService.GetOrdersByUser(userId));
    }
}