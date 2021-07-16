using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rigel.Business.Contracts
{
    public interface IBaseService<T> where T : class, IBaseEntity
    {
        Task<T> GetById(Guid entityId);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
