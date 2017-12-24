using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class BlogMapping : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable("Blog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.ImageIds).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsReleased).IsRequired();
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.UpdatedOn).IsRequired();
            builder.Ignore(x =>x.Customer);
        }
    }
}
