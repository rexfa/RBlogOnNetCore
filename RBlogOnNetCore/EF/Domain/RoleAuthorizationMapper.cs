using System;
using System.Collections.Generic;


namespace RBlogOnNetCore.EF.Domain
{
    public class RoleAuthorizationMapper : BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int AuthorizationId { get; set; }
        public Authorization Authorization { get; set; }
    }
}
