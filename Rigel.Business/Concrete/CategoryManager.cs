using Rigel.Business.Contracts;
using Rigel.Data.Contracts;
using Rigel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Business.Concrete
{
    public class CategoryManager : BaseManager<Category>, ICategoryService
    {
        public CategoryManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
