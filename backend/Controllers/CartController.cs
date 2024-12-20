using System.Security.Claims;
using backend.DTOs.CartItem;
using backend.DTOs.Product;
using backend.Models;
using backend.Services;
using backend.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CartController : Controller
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(List<PostCartItemsDto> cartItems)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _service.AddToCart(cartItems, userId));
    }
    
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetCartItemsDto>>>> GetCartProducts()
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _service.GetCartProducts(userId));
    }
    
    [HttpDelete("{productId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteCartItem(int productId)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _service.DeleteCartItem(userId, productId));
    }
    
    [HttpPut("{productId}")]
    public async Task<ActionResult<ServiceResponse<int>>> UpdateCartItemQuantity(int productId, [FromQuery] bool add)
    {
        var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return Ok(await _service.UpdateCartItemQuantity(userId, productId, add));
    }
    
}