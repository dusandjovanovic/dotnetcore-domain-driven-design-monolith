using System.Net.Mail;

namespace DDDMedical.Domain.Services
{
    public interface IMailService
    {
        void SendMail(MailMessage mailMessage);
    }
}