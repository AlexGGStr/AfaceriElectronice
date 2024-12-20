using backend.DTOs.CartItem;
using backend.DTOs.Product;
using backend.Models;

namespace backend.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<bool>> AddToCart(List<PostCartItemsDto> cartItems, int userId);
    
    Task<ServiceResponse<List<GetCartItemsDto>>> GetCartProducts(int userId);
    
    Task<ServiceResponse<bool>> DeleteCartItem(int userId, int productId);
    
    Task<ServiceResponse<int>> UpdateCartItemQuantity(int userId, int productId, bool add);
}