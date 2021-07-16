using Rigel.Data.RigelDB.Concretes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rigel.Data.RigelDB.Concretes.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string Email { get; set; }


        public DateTime LastActivity { get; set; }


        [MaxLength(15)]
        public string IpAddress { get; set; }


        public UserRole UserRole { get; set; }


        [MaxLength(10)]
        public string UserName { get; set; }


        [MaxLength(100)]
        public string Password { get; set; }

        public ICollection<Todo> Todoes { get; set; }
    }
}
