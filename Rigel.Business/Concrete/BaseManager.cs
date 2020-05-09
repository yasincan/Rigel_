using Rigel.Business.Contracts;
using Rigel.Data.Contracts;
using Rigel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rigel.Business.Concrete
{
    public class BaseManager<T> : IBaseService<T> where T : EntityBase, new()
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public virtual bool Delete(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            _unitOfWork.Repository<T>().Delete(entity);
            return _unitOfWork.SaveChanges() > 0;
        }

        public virtual T FindById(Guid entityId)
        {
            return _unitOfWork.Repository<T>().FindById(entityId);
        }

        public virtual T Insert(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            _unitOfWork.Repository<T>().Insert(entity);
            _unitOfWork.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<T> Select()
        {
            return _unitOfWork.Repository<T>().Select().Where(c => c.DeletedDate == null).OrderByDescending(c => c.CreatedDate);
        }

        public virtual bool Update(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _unitOfWork.Repository<T>().Update(entity);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
