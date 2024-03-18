using Core.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class MappingEntityTypeConfiguration<TEntity> : IMappingConfiguration, IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(this);
    }

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(p => p.CreatedAt).HasColumnType("datetime2").HasDefaultValueSql("GetUtcDate()");
        builder.Property(p => p.UpdatedAt).HasColumnType("datetime2").HasDefaultValueSql("GetUtcDate()");

        builder.Property(p => p.CreatedBy).HasColumnType("nvarchar(50)").HasDefaultValue(Defaults.AnonymousCreation);
        builder.Property(p => p.UpdatedBy).HasColumnType("nvarchar(50)").HasDefaultValue(Defaults.AnonymousCreation);
    }
}
