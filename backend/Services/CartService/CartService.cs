using AutoMapper;
using backend.DTOs.CartItem;
using backend.DTOs.Product;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.CartService;

public class CartService : ICartService
{
    private readonly AlexContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(AlexContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<bool>> AddToCart(List<PostCartItemsDto> cartItems, int userId)
    {
        var response = new ServiceResponse<bool>
        {
            Message = "Could not add items to cart",
            Success = false,
        };
        var storedItems = await _context.CartItems.Where(item => item.UserId == userId).ToListAsync();
        
        foreach (var item in cartItems)
        {
            var storedItem = storedItems.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (storedItem != null)
            {
                storedItem.Quantity += item.Quantity;
            }
            else
            {
                var cartItem = _mapper.Map<CartItem>(item);
                cartItem.UserId = userId;
                await _context.CartItems.AddAsync(cartItem);
            }
        }
        
        await _context.SaveChangesAsync();

        response.Data = true;
        response.Message = "Cart items added";
        response.Success = true;

        return response;
    }

    public async Task<ServiceResponse<List<GetCartItemsDto>>> GetCartProducts(int userId)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var response = new ServiceResponse<List<GetCartItemsDto>>();
        var data = await _context.CartItems.Where(item => item.UserId == userId)
            .Include(item => item.Product.Images).ToListAsync();
        
        var d = _mapper.Map<List<GetCartItemsDto>>(data);
        foreach (var prod in d)
        {
            foreach (var image in prod.product.Images)
            {
                image.ImageName = String.Format("{0}://{1}{2}/Images/{3}", request.Scheme, request.Host, request.PathBase, image.ImageName);
            }
        }
        response.Data = d;
        return response;
    }

    public async Task<ServiceResponse<bool>> DeleteCartItem(int userId, int productId)
    {
        var response = new ServiceResponse<bool>();
        var item = await _context.CartItems.FirstOrDefaultAsync(item => item.UserId == userId && item.ProductId == productId);
        if (item == null)
        {
            response.Success = false;
            response.Message = "Item not found";
        }
        else
        {
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            response.Success = true;
            response.Message = "Item removed";
        }
        return response;
    }

    public async Task<ServiceResponse<int>> UpdateCartItemQuantity(int userId, int productId, bool add)
    {
        var response = new ServiceResponse<int>();
        var item = await _context.CartItems
            .FirstOrDefaultAsync(item => item.UserId == userId && item.ProductId == productId);
        if (item == null)
        {
            response.Success = false;
            response.Message = "Item not found";
        }
        else
        {
            item.Quantity += add ? 1 : -1;
            await _context.SaveChangesAsync();
            response.Message = "Item updated";
            response.Data = item.Quantity;
        }
        return response;

    }
}