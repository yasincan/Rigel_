using AutoMapper;
using AutoMapper.QueryableExtensions;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.EntityFrameworkCore;
using Rigel.Business.Contracts;
using Rigel.Data.Contexts;
using Rigel.Data.Contracts;
using Rigel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rigel.Business.Concrete
{
    public class TodoManager : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RigelContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;
        public TodoManager(IUnitOfWork unitOfWork, RigelContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mappingConfiguration = mappingConfiguration;
        }
        public bool Delete(Todo todo)
        {
            todo.DeletedDate = DateTime.Now;
            _unitOfWork.Repository<Todo>().Delete(todo);
            return _unitOfWork.SaveChanges() > 0;
        }

        public Todo FindById(Guid id)
        {
            return _unitOfWork.Repository<Todo>().FindById(id);
        }

        public Todo Insert(Todo todo)
        {
            todo.CreatedDate = DateTime.Now;
            _unitOfWork.Repository<Todo>().Insert(todo);
            _unitOfWork.SaveChanges();
            return todo;
        }

        public IEnumerable<Todo> Select()
        {
            return _unitOfWork.Repository<Todo>().Select().Where(c => c.DeletedDate == null).OrderByDescending(c => c.CreatedDate);
        }

        public IEnumerable<Todo> ListTodo()
        {
            return _unitOfWork.Repository<Todo>().Select();
        }

        public bool Update(Todo todo)
        {
            todo.UpdatedDate = DateTime.Now;
            _unitOfWork.Repository<Todo>().Update(todo);
            return _unitOfWork.SaveChanges() > 0;
        }

        public async Task<JqueryDataTablesPagedResults<ViewModels.Todo>> GetDataAsync(JqueryDataTablesParameters table)
        {
            ViewModels.Todo[] items = null;
            IQueryable<Todo> query = _context.Todos.Where(d => d.DeletedDate == null).OrderByDescending(d => d.CreatedDate).AsNoTracking();

            query = SearchOptionsProcessor<ViewModels.Todo, Todo>.Apply(query, table.Columns);
            query = SortOptionsProcessor<ViewModels.Todo, Todo>.Apply(query, table);

            var size = await query.CountAsync();

            if (table.Length > 0)
            {
                items = await query
                .Skip((table.Start / table.Length) * table.Length)
                .Take(table.Length)
                .ProjectTo<ViewModels.Todo>(_mappingConfiguration)
                .ToArrayAsync();
            }
            else
            {
                items = await query
                .ProjectTo<ViewModels.Todo>(_mappingConfiguration)
                .ToArrayAsync();
            }

            return new JqueryDataTablesPagedResults<ViewModels.Todo>
            {
                Items = items,
                TotalSize = size
            };
        }
    }
}
