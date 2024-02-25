using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class CustomerProfileMap : MappingEntityTypeConfiguration<CustomerProfile>
{
    public override void Configure(EntityTypeBuilder<CustomerProfile> builder)
    {
        builder.ToTable("CustomerProfiles");
        builder.HasKey(p => p.Id);

        base.Configure(builder);
    }
}
