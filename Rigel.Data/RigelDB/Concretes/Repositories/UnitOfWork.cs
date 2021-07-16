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
        public IRepository<T> Repository<T>() where T : BaseEntity, new()
        {
            return new EFRepository<T>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = 1;
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Todos.Add(new Todo
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate=DateTime.Now,
                        Description="",
                        TodoName="test",
                        U
                    });
                    _context.SaveChanges();
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    //Log tutmak uygun olur
                    result = 0;
                    await dbContextTransaction.RollbackAsync();
                }
            }
            return result;
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
