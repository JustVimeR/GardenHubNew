using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class FeedbackProfile : MappingEntityTypeConfiguration<Feedback>
{
    public override void Configure(EntityTypeBuilder<Feedback> builder)
    {
        builder.ToTable("Feedbacks");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Customer).WithMany(p=>p.CustomerFeedbacks).HasForeignKey(p => p.CustomerId);
        builder.HasOne(p => p.Gardener).WithMany(p=>p.GardenerFeedbacks).HasForeignKey(p => p.GardenerId);

        base.Configure(builder);
    }
}

