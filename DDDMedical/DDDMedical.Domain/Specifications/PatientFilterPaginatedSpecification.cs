using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Specifications
{
    public class PatientFilterPaginatedSpecification : BaseSpecification<Patient>
    {
        public PatientFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}