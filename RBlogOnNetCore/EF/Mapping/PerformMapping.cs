using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class PerformMapping : IEntityTypeConfiguration<Perform>
    {
        public void Configure(EntityTypeBuilder<Perform> builder)
        {
            builder.ToTable("Perform");
            builder.HasKey(x => x.id);
            builder.Property(x => x.bigPicture).IsRequired().HasMaxLength(300);
            builder.Property(x => x.smallPicture).IsRequired().HasMaxLength(300);
            builder.Property(x => x.createdOn).IsRequired();
            builder.Property(x => x.description).IsRequired().HasMaxLength(300);
            builder.Property(x => x.name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.webUrl).IsRequired().HasMaxLength(300);
            builder.Property(x => x.sort).IsRequired();
            builder.Property(x => x.groupId).IsRequired();
        }
    }
}
