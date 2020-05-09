using Rigel.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rigel.Data.Entities
{
    public abstract class EntityBase: IEntityBase
    {
       public Guid Id { get; set; }
       public DateTime? DeletedDate { get; set; }
       public DateTime CreatedDate { get; set; }
       public DateTime? UpdatedDate { get; set; }
       public bool IsActive { get; set; }
    }
}
