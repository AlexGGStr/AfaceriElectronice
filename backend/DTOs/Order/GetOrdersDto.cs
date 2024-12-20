using backend.DTOs.UserAdress;
using backend.DTOs.UserPayment;
using backend.Models;

namespace backend.DTOs.Order;

public class GetOrdersDto
{
    public GetUserAdressDto Adress { get; set; }
    public GetUserPaymentDto Payment { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
    public int Total { get; set; }
}