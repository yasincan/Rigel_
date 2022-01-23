using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Rigel.Business.Models.Dtos;
using Rigel.Data.RigelDB.Concretes.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rigel.Business.Contracts
{
    public interface ITodoService
    {
        Task<JqueryDataTablesPagedResults<TodoDto>> GetDataTableAsync(JqueryDataTablesParameters table);
        Task<TodoDto> GetByIdAsync(Guid id);
        Task<TodoDto> AddAsync(TodoDto todo);
        Task<bool> UpdateAsync(TodoDto todo);
        Task<bool> DeleteAsync(TodoDto todo);
        Task<IEnumerable<TodoDto>> GetAllAsNoTrackingAsync();
    }
}
