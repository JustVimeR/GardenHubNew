using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class CityMap : MappingEntityTypeConfiguration<City>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");
        builder.HasKey(p => p.Id);

        base.Configure(builder);
    }
}
