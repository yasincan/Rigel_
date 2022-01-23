using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rigel.Data.RigelDB.Contracts
{
    public interface IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        IQueryable<T> Query();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> FindAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
