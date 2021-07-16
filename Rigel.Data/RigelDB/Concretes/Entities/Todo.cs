using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Rigel.Data.RigelDB.Concretes.Entities
{
    [Table("Todos")]
    public class Todo : BaseEntity
    {
        [MaxLength(255)]
        public string TodoName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public Guid UsersId { get; set; }
        public virtual User Users { get; set; }
    }
}
