using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ChatMessageMap : MappingEntityTypeConfiguration<ChatMessage>
{
    public override void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessage");
        builder.HasKey(p => p.Id);

        builder.Property(p=>p.PublicationDate).HasDefaultValueSql("GetUtcDate()");

        base.Configure(builder);
    }
}
