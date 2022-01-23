using System.Collections.Generic;

namespace Rigel.Business.Models.Dtos
{
    public class CategoryDto : BaseDto
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public IEnumerable<CategoryDto> SubCategories { get; set; }
    }
}
