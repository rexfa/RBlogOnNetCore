using System;
using System.Collections.Generic;


namespace RBlogOnNetCore.EF.Domain
{
    public class CustomerRoleMapper : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
