using AutoMapper;
using Rigel.Business.Models.Dtos;
using Rigel.Data.RigelDB.Concretes.Entities;

namespace Rigel.Business.Models.ModelMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Todo, TodoDto>();
            CreateMap<TodoDto, Todo>();

            CreateMap<Todo,Todo>();
        }
    }

}
