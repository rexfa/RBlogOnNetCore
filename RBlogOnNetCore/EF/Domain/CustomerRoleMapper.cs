using System.ComponentModel.DataAnnotations.Schema;


namespace RBlogOnNetCore.EF.Domain
{
    [Table("CustomerRoleMapper")]
    public class CustomerRoleMapper : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
