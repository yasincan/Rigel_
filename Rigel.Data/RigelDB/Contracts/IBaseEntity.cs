using System;

namespace Rigel.Data.RigelDB.Contracts
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime? DeletedDate { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        bool IsActive { get; set; }
    }
}
