using Rigel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rigel.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : EntityBase, new();
        int SaveChanges();
    }
}
