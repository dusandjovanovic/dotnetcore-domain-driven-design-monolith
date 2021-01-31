using System.Net.Mail;

namespace DDDMedical.Domain.Services
{
    public class MailService : IMailService
    {
        public MailService() {}
        
        public void SendMail(MailMessage mailMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}