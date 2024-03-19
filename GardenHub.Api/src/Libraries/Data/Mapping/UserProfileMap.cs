using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class UserProfileMap : MappingEntityTypeConfiguration<UserProfile>
{
    public override void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable("UserProfiles");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description).HasMaxLength(1000);
        builder.Property(p => p.DescriptionOfExperience).HasMaxLength(1000);

        builder.HasMany(p => p.GardenerProjects).WithMany(p => p.Gardeners);

        builder.HasMany(p => p.CustomerProjects)
               .WithOne(p => p.Customer)
               .HasForeignKey(p => p.CustomerId)
               .OnDelete(DeleteBehavior.NoAction);

        base.Configure(builder);
    }
}
