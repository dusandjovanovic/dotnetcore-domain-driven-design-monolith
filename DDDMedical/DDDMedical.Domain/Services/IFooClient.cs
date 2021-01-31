using System.Threading.Tasks;
using Refit;

namespace DDDMedical.Domain.Services
{
    public interface IFooClient
    {
        [Get("/")]
        Task<object> GetAll();
    }
}