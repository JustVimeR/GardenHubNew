using Models.Enums;
using System;

namespace Models.DbEntities;

public interface IEntityBase
{
    long Id { get; set; }

    RecordStatus? RecordStatus { get; set; }
    DateTime? CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }

    string? CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
}
