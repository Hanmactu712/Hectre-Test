using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Hectre.BackEnd.Data
{
    /// <summary>
    /// generic repository for an entity model against corresponding database context
    /// </summary>
    /// <typeparam name="TEntity">entity model</typeparam>
    /// <typeparam name="TDbContext">database context which contains the entity model</typeparam>
    public class EFRepository<TEntity, TDbContext> : IEfRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public EFRepository(TDbContext context)
        {
            _context = context;
        }

        public TEntity GetById(string id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IReadOnlyList<TEntity> ListAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public IReadOnlyList<TEntity> List(ISpecification<TEntity> spec)
        {
            var specResult = ApplySpecification(spec);
            return specResult.ToList();
        }

        public TEntity Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Update(TEntity entity)
        {
            var updateEntity = _context.Entry(entity);
            updateEntity.State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public int Count(ISpecification<TEntity> spec)
        {
            var specResult = ApplySpecification(spec);
            return specResult.Count();
        }

        public TEntity First(ISpecification<TEntity> spec)
        {
            var specResult = ApplySpecification(spec);
            return specResult.First();

        }

        public TEntity FirstOrDefault(ISpecification<TEntity> spec)
        {
            var specResult = ApplySpecification(spec);
            return specResult.FirstOrDefault();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator.Default.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
        }
    }
}
