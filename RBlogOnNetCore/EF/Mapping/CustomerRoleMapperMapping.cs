using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;


namespace RBlogOnNetCore.EF.Mapping
{
    public class CustomerRoleMapperMapping : IEntityTypeConfiguration<CustomerRoleMapper>
    {
        public void Configure(EntityTypeBuilder<CustomerRoleMapper> builder)
        {
            builder.ToTable(" CustomerRoleMapper");
            builder.HasKey(x => x.Id);
        }
    }
}
