using DDDMedical.Domain.Interfaces;
using DDDMedical.Infrastructure.Data.Context;

namespace DDDMedical.Infrastructure.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }
        
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}