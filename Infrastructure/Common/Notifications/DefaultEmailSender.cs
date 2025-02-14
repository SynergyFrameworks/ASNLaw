using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class DefaultEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //nothing to do
            return Task.CompletedTask;
        }
    }
}
