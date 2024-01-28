using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping
{
    public class CustomerProfileMap : MappingEntityTypeConfiguration<CustomerProfile>
    {
        public override void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.ToTable("CustomerProfiles");
            builder.HasKey(p => p.Id);
            //builder.Property(p => p.).HasMaxLength(1000);

            base.Configure(builder);
        }
    }
}
