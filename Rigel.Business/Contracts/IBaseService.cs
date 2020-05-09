using Rigel.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Business.Contracts
{
    public interface IBaseService<T> where T : class, IEntityBase
    {
        T FindById(Guid entityId);
        IEnumerable<T> Select();
        T Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
    }
}
