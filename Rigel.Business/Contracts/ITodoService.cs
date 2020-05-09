using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Rigel.Data.Entities;

namespace Rigel.Business.Contracts
{
    public interface ITodoService
    {
        Task<JqueryDataTablesPagedResults<ViewModels.Todo>> GetDataAsync(JqueryDataTablesParameters table);
        Todo FindById(Guid id);
        IEnumerable<Todo> Select();
        Todo Insert(Todo todo);
        bool Update(Todo todo);
        bool Delete(Todo todo);
    }
}
