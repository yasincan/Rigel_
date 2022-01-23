using Rigel.Data.RigelDB.Concretes.Entities;
using System;
using System.Threading.Tasks;

namespace Rigel.Data.RigelDB.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : BaseEntity, new();
        Task<int> SaveChangesAsync();
    }
}
