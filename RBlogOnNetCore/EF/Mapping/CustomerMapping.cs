using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.EF.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.id);
            builder.Property(x => x.name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.guid).IsRequired().HasMaxLength(16);
            builder.Property(x => x.password).IsRequired().HasMaxLength(50);
            builder.Property(x => x.salt).HasMaxLength(10);
            builder.Property(x => x.createdOn).IsRequired();
            //builder.Ignore(x => x.Blogs);
            builder.HasMany(x => x.Blogs).WithOne(x => x.Customer).HasForeignKey(x => x.customerId).OnDelete(DeleteBehavior.Restrict);
            //builder.Ignore(x => x.Pictures);
            builder.HasMany(x => x.Pictures).WithOne(x => x.Customer).HasForeignKey(x => x.customerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
