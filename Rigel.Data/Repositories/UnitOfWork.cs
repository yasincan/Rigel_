using Rigel.Data.Contexts;
using Rigel.Data.Contracts;
using Rigel.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Rigel.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RigelContext _context;

        public UnitOfWork(RigelContext context)
        {
            _context = context;
        }
        public IRepository<T> Repository<T>() where T : EntityBase, new()
        {
            return new Repository<T>(_context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
