using Rigel.Business.Contracts;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rigel.Business.Concrete
{
    public class BaseManager<T> : IBaseService<T> where T : BaseEntity, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity = await _unitOfWork.Repository<T>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            _unitOfWork.Repository<T>().DeleteAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _unitOfWork.Repository<T>().FindAsync(filter);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _unitOfWork.Repository<T>().GetAllAsync(filter);
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
             entity.UpdatedDate = DateTime.Now;
            _unitOfWork.Repository<T>().UpdateAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
