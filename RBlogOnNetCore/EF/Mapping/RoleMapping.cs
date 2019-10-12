using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.ToTable("Role");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.IsSystem).IsRequired().HasDefaultValue(false);
       }
    }
}
