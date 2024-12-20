using backend.DTOs.Product;

namespace backend.DTOs.CartItem;

public class GetCartItemsDto
{
    public GetProductDto product { get; set; }
    
    public int Quantity { get; set; }
}