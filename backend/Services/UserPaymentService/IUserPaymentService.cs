using backend.DTOs.UserAdress;
using backend.DTOs.UserPayment;

namespace backend.Services.UserPaymentService;

public interface IUserPaymentService
{
    Task<ServiceResponse<List<GetUserPaymentDto>>> GetAllUserPayments(int userId);
    
    Task<ServiceResponse<GetUserPaymentDto>> GetUserPaymentById(int userId, int id);
    
    Task <ServiceResponse<int>> UpdateUserPayment(int userId, int id, PostPutUserPaymentDto updatedUserPayment);
    
    Task<ServiceResponse<int>> DeleteUserPayment(int userId, int id);
    
    Task<ServiceResponse<int>> AddUserPayment(int userId, PostPutUserPaymentDto newUserPayment);
    
}