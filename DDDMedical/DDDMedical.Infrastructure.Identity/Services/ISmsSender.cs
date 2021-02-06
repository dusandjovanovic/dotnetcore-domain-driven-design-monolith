using System.Threading.Tasks;

namespace DDDMedical.Infrastructure.Identity.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}