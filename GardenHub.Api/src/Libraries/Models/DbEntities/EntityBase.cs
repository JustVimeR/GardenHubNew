using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.DbEntities;

public abstract class EntityBase : IEntityBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public RecordStatus? RecordStatus { get; set; }

    private DateTime? createdAt;
    private DateTime? updatedAt;

    public DateTime? CreatedAt
    {
        get => createdAt != null ? DateTime.SpecifyKind(createdAt.Value, DateTimeKind.Utc) : null;
        set => createdAt = value;
    }
    public DateTime? UpdatedAt
    {
        get => updatedAt != null ? DateTime.SpecifyKind(updatedAt.Value, DateTimeKind.Utc) : null;
        set => updatedAt = value;
    }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
