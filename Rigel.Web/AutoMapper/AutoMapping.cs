using AutoMapper;

namespace Rigel.Web.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Data.Entities.Category, ViewModels.Category>();
            CreateMap<ViewModels.Category, Data.Entities.Category>();

            CreateMap<Data.Entities.Todo, ViewModels.Todo>();
            CreateMap<ViewModels.Todo, Data.Entities.Todo>();
        }
    }

}
