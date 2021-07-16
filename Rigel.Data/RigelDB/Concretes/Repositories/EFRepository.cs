using Microsoft.EntityFrameworkCore;
using Rigel.Data.RigelDB.Concretes.Context;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rigel.Data.RigelDB.Concretes.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly RigelContext _context;
        private readonly DbSet<T> _dbSet;

        public EFRepository(RigelContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual Task<T> FindByIdAsyn(Guid id)
        {
            return _dbSet.SingleOrDefaultAsync(e => e.Id == id);
        }
        public virtual IQueryable<T> Query(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return _dbSet.Where(filter);
            return _dbSet;
        }
        public virtual async Task<T> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public virtual void UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
