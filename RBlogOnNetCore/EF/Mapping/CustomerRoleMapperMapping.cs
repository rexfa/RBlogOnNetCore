using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;


namespace RBlogOnNetCore.EF.Mapping
{
    public class CustomerRoleMapperMapping : IEntityTypeConfiguration<CustomerRoleMapper>
    {
        public void Configure(EntityTypeBuilder<CustomerRoleMapper> builder)
        {
            builder.ToTable("CustomerRoleMapper");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Customer).WithMany(x => x.CustomerRoleMapper).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Role).WithMany(x => x.CustomerRoleMapper).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
