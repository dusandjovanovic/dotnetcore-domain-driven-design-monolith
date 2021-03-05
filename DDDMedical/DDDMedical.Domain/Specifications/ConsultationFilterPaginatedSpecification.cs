using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Specifications
{
    public class ConsultationFilterPaginatedSpecification : BaseSpecification<Consultation>
    {
        public ConsultationFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}