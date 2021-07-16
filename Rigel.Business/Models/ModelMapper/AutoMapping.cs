using AutoMapper;
using Rigel.Business.Models.ViewModels;
using Rigel.Data.RigelDB.Concretes.Entities;

namespace Rigel.Business.Models.ModelMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();

            CreateMap<Todo, TodoViewModel>();
            CreateMap<TodoViewModel, Todo>();

            CreateMap<Todo,Todo>();
        }
    }

}
