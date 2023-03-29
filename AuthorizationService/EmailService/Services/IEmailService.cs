using EmailService.Models;

namespace EmailService.Services
{
    public interface IEmailService
    {
        Task SendEmail(Message message);
    }
}
