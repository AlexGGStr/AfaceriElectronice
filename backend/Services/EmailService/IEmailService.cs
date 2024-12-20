using backend.DTOs.Email;

namespace backend.Services.EmailService;

public interface IEmailService
{
    void SendEmail(Email info);
}