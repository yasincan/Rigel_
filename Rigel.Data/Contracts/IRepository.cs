using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Rigel.Data.Contracts
{
    public interface IRepository<T> where T : IEntityBase
    {
        T FindById(Guid EntityId);
        IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null);
        T Insert(T Entity);
        void Update(T Entity);
        void Delete(Guid EntityId);
        void Delete(T Entity);
    }
}
