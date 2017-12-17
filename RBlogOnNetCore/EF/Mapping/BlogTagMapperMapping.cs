using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class BlogTagMapperMapping : IEntityTypeConfiguration<BlogTagMapper>
    {
        public void Configure(EntityTypeBuilder<BlogTagMapper> builder)
        {
            builder.ToTable("BlogTagMapper");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Blog).WithMany(x => x.BlogTagMappers).HasForeignKey(x => x.BlogId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Tag).WithMany(x => x.BlogTagMappers).HasForeignKey(x => x.TagId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
