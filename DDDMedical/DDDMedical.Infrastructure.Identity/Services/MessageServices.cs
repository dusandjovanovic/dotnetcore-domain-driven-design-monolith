using System.Threading.Tasks;

namespace DDDMedical.Infrastructure.Identity.Services
{
    public class AuthEmailMessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }
    }

    public class AuthSmsMessageSender : ISmsSender
    {
        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}