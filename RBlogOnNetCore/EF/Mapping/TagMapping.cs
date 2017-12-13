using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");
            builder.HasKey(x => x.id);
            builder.Property(x => x.tag).IsRequired().HasMaxLength(30);
            builder.Property(x => x.referenceNum).IsRequired();
            builder.Property(x => x.createdOn).IsRequired();
            builder.Property(x => x.lastReferencedOn).IsRequired();
        }
    }
}
