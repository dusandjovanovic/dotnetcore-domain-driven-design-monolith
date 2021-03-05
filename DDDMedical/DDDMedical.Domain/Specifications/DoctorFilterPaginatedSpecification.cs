using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Specifications
{
    public class DoctorFilterPaginatedSpecification : BaseSpecification<Doctor>
    {
        public DoctorFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}