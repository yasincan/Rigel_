using System;

namespace Rigel.Data.Contracts
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        DateTime? DeletedDate { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
        bool IsActive { get; set; }
    }
}
