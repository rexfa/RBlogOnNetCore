using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class NormalCommentMapping : IEntityTypeConfiguration<NormalComment>
    {
        public void Configure(EntityTypeBuilder<NormalComment> builder)
        {
            //builder.ToTable("NormalComment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nikename).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CommentText).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.HomepageUrl).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PreIds).IsRequired().HasDefaultValue("").HasMaxLength(100);
            builder.Property(x => x.BlogId).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
