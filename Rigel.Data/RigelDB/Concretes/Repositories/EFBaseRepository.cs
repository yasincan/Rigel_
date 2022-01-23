using Microsoft.EntityFrameworkCore;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rigel.Data.RigelDB.Concretes.Repositories
{
    public class EFBaseRepository<T, TDbContext> : IBaseRepository<T>
        where T : class, IBaseEntity, new()
        where TDbContext : DbContext, new()
    {
        private readonly TDbContext _context;

        public EFBaseRepository(TDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter == null)
                return await _context.Set<T>().ToListAsync();
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
