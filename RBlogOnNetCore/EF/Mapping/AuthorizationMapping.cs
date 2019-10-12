using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;
namespace RBlogOnNetCore.EF.Mapping
{
    public class AuthorizationMapping : IEntityTypeConfiguration<Authorization>
    {
        public void Configure(EntityTypeBuilder<Authorization> builder)
        {
            //builder.ToTable("Authorization");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.URL).IsRequired(required:false).HasMaxLength(500);
            builder.Property(x => x.URLController).IsRequired(required: false).HasMaxLength(100);
            builder.Property(x => x.URLAction).IsRequired(required: false).HasMaxLength(100);
            builder.Property(x => x.IsNeedAuthorization).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.UnauthorizedJump).IsRequired(required: false).HasMaxLength(500);
        }
    }
}
