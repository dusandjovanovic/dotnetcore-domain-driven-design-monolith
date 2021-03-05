using DDDMedical.Domain.Interfaces;
using DDDMedical.Domain.Models;
using DDDMedical.Infrastructure.Data.Context;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class ConsultationRepository : Repository<Consultation>, IConsultationRepository
    {
        public ConsultationRepository(ApplicationDbContext context) : base(context) { }
    }
}