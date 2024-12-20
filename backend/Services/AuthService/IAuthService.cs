using backend.DTOs.User;
using backend.Models;

namespace backend.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register (UserRegisterDto user);
    Task<ServiceResponse<string>> Login(string username, string password);
    Task<bool> UserExists(string username);
    Task<ServiceResponse<string>> AuthenticateWithGoogle(string googleToken);
    
    Task<ServiceResponse<int>> VerifyUser(string token);
}