using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DDDMedical.Infrastructure.Identity.Services;

namespace DDDMedical.Infrastructure.Identity.Extensions
{
    public static class EmailSenderExtension
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email.",
                $"Please confirm your registration <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}