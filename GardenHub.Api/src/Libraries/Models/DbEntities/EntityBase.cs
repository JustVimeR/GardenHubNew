using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models.DbEntities
{
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public RecordStatus RecordStatus { get; set; }

        private DateTime createdAt;
        private DateTime updatedAt;

        public required DateTime CreatedAt
        {
            get => DateTime.SpecifyKind(createdAt, DateTimeKind.Utc);
            set => createdAt = value;
        }
        public required DateTime UpdatedAt
        {
            get => DateTime.SpecifyKind(updatedAt, DateTimeKind.Utc);
            set => updatedAt = value;
        }

        public required string CreatedBy { get; set; } = string.Empty;
        public required string UpdatedBy { get; set; } = string.Empty;
    }
}
