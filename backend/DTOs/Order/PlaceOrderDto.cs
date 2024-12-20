using backend.DTOs.UserAdress;
using backend.DTOs.UserPayment;
using backend.Models;

namespace backend.DTOs.Order;

public class PlaceOrderDto
{
    public PostUserAdressDto adress { get; set; }
    public PostPutUserPaymentDto payment { get; set; }
    public int total { get; set; }
}