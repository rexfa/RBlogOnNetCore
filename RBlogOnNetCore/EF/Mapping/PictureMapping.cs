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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.LocalName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.CustomName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.OriginalName).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PicType).IsRequired().HasMaxLength(50);
            builder.Property(x => x.UpdatedOn).IsRequired();
            builder.Ignore(x => x.Customer);
        }
    }
}
