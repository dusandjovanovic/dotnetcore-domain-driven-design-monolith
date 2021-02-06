using System.Threading.Tasks;

namespace DDDMedical.Infrastructure.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}