using Microsoft.EntityFrameworkCore;
using Rigel.Business.Contracts;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public virtual async Task<bool> DeleteAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            _unitOfWork.Repository<T>().DeleteAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public virtual async Task<T> GetById(Guid entityId)
        {
            return await _unitOfWork.Repository<T>().FindByIdAsyn(entityId);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;
            entity = await _unitOfWork.Repository<T>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _unitOfWork.Repository<T>().Query(c => c.DeletedDate == null).OrderByDescending(c => c.CreatedDate).ToListAsync();
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _unitOfWork.Repository<T>().UpdateAsync(entity);
            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }
}
