using backend.DTOs.Product;

namespace backend.DTOs.Order;

public class OrderItemDto
{
    public GetProductDto Product;

    public int Quantity { get; set; }
}