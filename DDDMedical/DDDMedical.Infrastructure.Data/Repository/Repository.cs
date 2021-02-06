using System;
using System.Linq;
using DDDMedical.Domain.Interfaces;
using DDDMedical.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DDDMedical.Infrastructure.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity obj)
        {
            _dbSet.Add(obj);
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAll(ISpecification<TEntity> specification)
        {
            return ApplySpecification(specification);
        }

        public virtual IQueryable<TEntity> GetAllSoftDeleted()
        {
            return _dbSet.IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDeleted"));
        }

        public void Update(TEntity obj)
        {
            _dbSet.Update(obj);
        }

        public void Remove(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), specification);
        }
    }
}