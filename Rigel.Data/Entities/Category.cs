using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rigel.Data.Entities
{
    [Table("Categories")]
    public class Category : EntityBase
    {
        [MaxLength(20)]
        public string CategoryName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public int DisplayOrder { get; set; }
        public IEnumerable<Category> SubCategories { get; set; }
    }
}
