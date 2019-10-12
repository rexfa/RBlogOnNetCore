using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class TagMapping : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //builder.ToTable("Tag", schema: "Tag");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TagName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.ReferenceNum).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.LastReferencedOn).IsRequired();
        }
    }
}
