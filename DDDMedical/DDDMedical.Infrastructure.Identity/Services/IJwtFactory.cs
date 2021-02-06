using System.Security.Claims;
using System.Threading.Tasks;

namespace DDDMedical.Infrastructure.Identity.Services
{
    public interface IJwtFactory
    {
        Task<JwtToken> GenerateJwtToken(ClaimsIdentity claimsIdentity);
    }
}