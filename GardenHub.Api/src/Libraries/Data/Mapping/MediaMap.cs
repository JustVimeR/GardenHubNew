using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class MediaMap : MappingEntityTypeConfiguration<Media>
{
    public override void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("Medias");
        builder.HasKey(p => p.Id);

        base.Configure(builder);
    }
}
