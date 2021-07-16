using Rigel.Data.RigelDB.Contracts;
using System;

namespace Rigel.Data.RigelDB.Concretes.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
       public Guid Id { get; set; }
       public DateTime? DeletedDate { get; set; }
       public DateTime CreatedDate { get; set; }
       public DateTime? UpdatedDate { get; set; }
       public bool IsActive { get; set; }
    }
}
