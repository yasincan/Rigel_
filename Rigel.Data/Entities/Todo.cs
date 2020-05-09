using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Rigel.Data.Entities
{
    [Table("Todos")]
    public class Todo : EntityBase
    {
        [MaxLength(255)]
        public string TodoName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Guid UsersId { get; set; }
        public virtual User Users { get; set; }
    }
}
