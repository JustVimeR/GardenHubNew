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

        base.Configure(builder);
    }
}

