using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class PictureMapping : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Picture");
            builder.HasKey(x => x.id);
            builder.Property(x => x.localName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.updateName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.originalName).IsRequired().HasMaxLength(255);
            builder.Ignore(x => x.Customer);
        }
    }
}
