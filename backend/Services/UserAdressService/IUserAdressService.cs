using backend.DTOs.UserAdress;

namespace backend.Services.UserAdressService;

public interface IUserAdressService
{
    Task<ServiceResponse<List<GetUserAdressDto>>> GetAllUserAdresses(int userId);
    Task<ServiceResponse<GetUserAdressDto>> GetUserAdressById(int userId, int id);
    
    Task <ServiceResponse<int>> UpdateUserAdress(int userId, int id, PutUserAdressDto updatedUserAdress);
    
    Task<ServiceResponse<int>> DeleteUserAdress(int userId, int id);
    
    //post a new adress
    Task<ServiceResponse<int>> AddUserAdress(int userId, PostUserAdressDto newUserAdress);
}