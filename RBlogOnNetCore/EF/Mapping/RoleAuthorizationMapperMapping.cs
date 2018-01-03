using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class RoleAuthorizationMapperMapping : IEntityTypeConfiguration<RoleAuthorizationMapper>
    {
        public void Configure(EntityTypeBuilder<RoleAuthorizationMapper> builder)
        {
            builder.ToTable(" RoleAuthorizationMapper");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Authorization).WithMany(x => x.RoleAuthorizationMapper).HasForeignKey(x => x.AuthorizationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Role).WithMany(x => x.RoleAuthorizationMapper).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
