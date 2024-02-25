using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ProjectMap : MappingEntityTypeConfiguration<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.Budget).HasColumnType("decimal(18, 2)");

        base.Configure(builder);
    }
}
