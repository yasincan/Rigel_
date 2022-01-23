using Rigel.Data.RigelDB.Concretes.Context;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.Threading.Tasks;

namespace Rigel.Data.RigelDB.Concretes.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RigelContext _context;

        public UnitOfWork(RigelContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> Repository<T>() where T : BaseEntity, new()
        {
            return new EFBaseRepository<T, RigelContext>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = 1;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    result = 0;
                    await dbContextTransaction.RollbackAsync();
                }
            }
            return result;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
