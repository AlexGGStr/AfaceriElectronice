using backend.DTOs.Order;

namespace backend.Services.PlaceOrder;

public interface IPlaceOrderService
{
    Task<ServiceResponse<int>> PlaceOrder(int UserId, PlaceOrderDto order);
    Task<ServiceResponse<List<GetOrdersDto>>> GetOrdersByUser(int UserId);
}