using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbEntities;

namespace Data.Mapping
{
    public class WorkTypeMap : MappingEntityTypeConfiguration<WorkType>
    {
        public override void Configure(EntityTypeBuilder<WorkType> builder)
        {
            builder.ToTable("WorkTypes");
            builder.HasKey(p => p.Id);
            
            base.Configure(builder);
        }
    }
}
