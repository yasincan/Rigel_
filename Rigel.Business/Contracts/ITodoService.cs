using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Rigel.Data.RigelDB.Concretes.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rigel.Business.Contracts
{
    public interface ITodoService
    {
        Task<JqueryDataTablesPagedResults<Todo>> GetDataTableAsync(JqueryDataTablesParameters table);
        Task<Todo> GetByIdAsync(Guid id);
        Task<Todo> AddAsync(Todo todo);
        Task<bool> UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(Todo todo);
        Task<IEnumerable<Todo>> GetAllAsNoTrackingAsync();
    }
}
