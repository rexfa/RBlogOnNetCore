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
            builder.HasKey(x => x.id);
            builder.Property(x => x.tilte).IsRequired().HasMaxLength(200);
            builder.Property(x => x.content).IsRequired();
            builder.Property(x => x.customerId).IsRequired();
            builder.Property(x => x.isDeleted).IsRequired();
            builder.Property(x => x.isReleased).IsRequired();
            builder.Property(x => x.createdOn).IsRequired();
            builder.Property(x => x.updatedOn).IsRequired();
            builder.Ignore(x =>x.Customer);
        }
    }
}
