using AutoMapper;
using AutoMapper.QueryableExtensions;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.EntityFrameworkCore;
using Rigel.Business.Contracts;
using Rigel.Business.Models.Dtos;
using Rigel.Data.RigelDB.Concretes.Context;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rigel.Business.Concrete
{
    public class TodoManager : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TodoManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(TodoDto todo)
        {
            todo.DeletedDate = DateTime.Now;
            _unitOfWork.Repository<Todo>().DeleteAsync(_mapper.Map<Todo>(todo));
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<TodoDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<TodoDto>(await _unitOfWork.Repository<Todo>().FindAsync(x => x.Id == id));
        }

        public async Task<TodoDto> AddAsync(TodoDto todo)
        {
            todo.Id = Guid.NewGuid();
            todo.CreatedDate = DateTime.Now;
            todo = _mapper.Map<TodoDto>(await _unitOfWork.Repository<Todo>().AddAsync(_mapper.Map<Todo>(todo)));
            await _unitOfWork.SaveChangesAsync();
            return todo;
        }

        public async Task<IEnumerable<TodoDto>> GetAllAsNoTrackingAsync()
        {
            return _mapper.Map<IEnumerable<TodoDto>>(await _unitOfWork.Repository<Todo>().Query().Where(c => c.DeletedDate == null).OrderByDescending(c => c.CreatedDate).AsNoTracking().ToListAsync());
        }

        public async Task<bool> UpdateAsync(TodoDto todo)
        {
            todo.UpdatedDate = DateTime.Now;
            _unitOfWork.Repository<Todo>().UpdateAsync(_mapper.Map<Todo>(todo));
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<JqueryDataTablesPagedResults<TodoDto>> GetDataTableAsync(JqueryDataTablesParameters table)
        {
          
            Todo[] items = null;
            IQueryable<Todo> query = _unitOfWork.Repository<Todo>().Query().Where(d => d.DeletedDate == null).OrderByDescending(d => d.CreatedDate).AsNoTracking();

            query = SearchOptionsProcessor<Todo, Todo>.Apply(query, table.Columns);
            query = SortOptionsProcessor<Todo, Todo>.Apply(query, table);

            var size = await query.CountAsync();

            if (table.Length > 0)
            {
                items = await query
                .Skip((table.Start / table.Length) * table.Length)
                .Take(table.Length)
                .ProjectTo<Todo>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
            }
            else
            {
                items = await query
                .ProjectTo<Todo>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
            }

            return new JqueryDataTablesPagedResults<TodoDto>
            {
                Items = _mapper.Map<IEnumerable<TodoDto>>(items),
                TotalSize = size
            };
        }
    }
}
