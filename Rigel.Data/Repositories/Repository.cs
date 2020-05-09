using Microsoft.EntityFrameworkCore;
using Rigel.Data.Contexts;
using Rigel.Data.Contracts;
using Rigel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rigel.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase, new()
    {
        private readonly RigelContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(RigelContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual T FindById(Guid EntityId)
        {
            return _dbSet.Find(EntityId);
        }

        public virtual IEnumerable<T> Select(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return _dbSet.Where(Filter);
            }
            return _dbSet.ToList();
        }

        public virtual T Insert(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(Guid entityId)
        {
            T entityToDelete = _dbSet.Find(entityId);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
