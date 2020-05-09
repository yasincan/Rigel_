using JqueryDataTables.ServerSide.AspNetCoreWeb.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rigel.ViewModels
{
    public class Category : BaseModel
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public IEnumerable<Category> SubCategories { get; set; }
    }
}
