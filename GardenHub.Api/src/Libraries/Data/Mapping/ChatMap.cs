using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping;

public class ChatMap : MappingEntityTypeConfiguration<Chat>
{
    public override void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");
        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.User1)
            .WithMany()
            .HasForeignKey(p => p.User1Id);

        builder.HasOne(p => p.User2)
            .WithMany()
            .HasForeignKey(p => p.User2Id);

        base.Configure(builder);
    }
}
