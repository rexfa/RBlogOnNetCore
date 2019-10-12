using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class PerformMapping : IEntityTypeConfiguration<Perform>
    {
        public void Configure(EntityTypeBuilder<Perform> builder)
        {
            //builder.ToTable("Perform");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BigPicture).IsRequired().HasMaxLength(300);
            builder.Property(x => x.SmallPicture).IsRequired().HasMaxLength(300);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.WebUrl).IsRequired().HasMaxLength(300);
            builder.Property(x => x.Sort).IsRequired();
            builder.Property(x => x.GroupId).IsRequired();
        }
    }
}
