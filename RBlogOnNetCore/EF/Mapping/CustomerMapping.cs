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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Guid).IsRequired().HasMaxLength(16);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Salt).HasMaxLength(10);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.CustomerName).IsRequired().HasMaxLength(50);
            //builder.Ignore(x => x.Blogs);
            builder.HasMany(x => x.Blogs).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
            //builder.Ignore(x => x.Pictures);
            builder.HasMany(x => x.Pictures).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
