using System;
using System.Linq;

namespace DDDMedical.Domain.Interfaces
{
    public interface IRepository<TEntity>: IDisposable where TEntity: class
    {
        void Add(TEntity obj);
        
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(ISpecification<TEntity> specification);
        IQueryable<TEntity> GetAllSoftDeleted();

        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }
}