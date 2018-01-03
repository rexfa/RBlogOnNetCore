using System;
using System.Collections.Generic;
namespace RBlogOnNetCore.EF.Domain
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public bool IsSystem { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CustomerRoleMapper> CustomerRoleMapper { get; set; }
        public virtual ICollection<RoleAuthorizationMapper> RoleAuthorizationMapper { get; set; }
    }
}
