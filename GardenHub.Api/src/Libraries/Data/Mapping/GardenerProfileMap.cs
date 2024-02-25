using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class GardenerProfileMap : MappingEntityTypeConfiguration<GardenerProfile>
{
    public override void Configure(EntityTypeBuilder<GardenerProfile> builder)
    {
        builder.ToTable("GardenerProfiles");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.DescriptionOfExperience).HasMaxLength(1000);

        base.Configure(builder);
    }
}

