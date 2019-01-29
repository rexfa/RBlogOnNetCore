using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class BlogBannerMapping : IEntityTypeConfiguration<BlogBanner>
    {
        public void Configure(EntityTypeBuilder<BlogBanner> builder)
        {
            builder.ToTable("BlogBanner");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.PicUrl).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Url).IsRequired().HasMaxLength(255);
            builder.Property(x => x.SortNum).IsRequired().HasDefaultValue(99);
            builder.Property(x => x.Type).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.RetiredOn).IsRequired();
        }
    }
}
