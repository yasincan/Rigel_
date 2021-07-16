using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rigel.Data.RigelDB.Contracts
{
    public interface IRepository<T> where T : IBaseEntity
    {
        Task<T> FindByIdAsyn(Guid id);
        IQueryable<T> Query(Expression<Func<T, bool>> filter = null);
        Task<T> InsertAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
