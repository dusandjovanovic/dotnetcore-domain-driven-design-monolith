using DDDMedical.Domain.Models;

namespace DDDMedical.Domain.Specifications
{
    public class TreatmentRoomFilterPaginatedSpecification : BaseSpecification<TreatmentRoom>
    {
        public TreatmentRoomFilterPaginatedSpecification(int skip, int take)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}