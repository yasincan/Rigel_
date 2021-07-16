using System.Collections.Generic;

namespace Rigel.Business.Models.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public IEnumerable<CategoryViewModel> SubCategories { get; set; }
    }
}
