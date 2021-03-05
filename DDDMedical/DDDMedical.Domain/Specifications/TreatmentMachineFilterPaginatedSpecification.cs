using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Specifications
{
    public class TreatmentMachineFilterPaginatedSpecification: BaseSpecification<TreatmentMachine>
    {
        public TreatmentMachineFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}