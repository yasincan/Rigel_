using Rigel.Business.Contracts;
using Rigel.Data.RigelDB.Concretes.Entities;
using Rigel.Data.RigelDB.Contracts;

namespace Rigel.Business.Concrete
{
    public class CategoryManager : BaseManager<Category>, ICategoryService
    {
        public CategoryManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
