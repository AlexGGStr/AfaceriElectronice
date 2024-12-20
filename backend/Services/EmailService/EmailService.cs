using backend.DTOs.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace backend.Services.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(Email info)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(info.To));
            email.Subject = info.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = info.Body };

            using var stmp = new SmtpClient();
            stmp.Connect(_configuration.GetSection("EmailHost").Value,
                Convert.ToInt32(_configuration.GetSection("EmailPort").Value), SecureSocketOptions.StartTls);
            stmp.Authenticate(_configuration.GetSection("EmailUsername").Value,
                _configuration.GetSection("EmailPassword").Value);
            stmp.Send(email);
            stmp.Disconnect(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}