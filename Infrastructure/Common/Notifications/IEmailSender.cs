using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
