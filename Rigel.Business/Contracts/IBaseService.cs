﻿using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rigel.Business.Contracts
{
    public interface IBaseService<T> where T : class, IBaseEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> FindAsync(Expression<Func<T, bool>> filter = null);
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
