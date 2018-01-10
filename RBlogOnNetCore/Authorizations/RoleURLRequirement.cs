using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Authorizations
{
    public class RoleURLRequirement : IAuthorizationRequirement
    {
        public RoleURLRequirement(string roleName)
        {
            RoleName = roleName;
        }
        public string RoleName { get; set; }
    }
}
