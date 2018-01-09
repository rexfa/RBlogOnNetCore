using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RBlogOnNetCore.Authorizations
{
    public class RoleURLAuthorizationHandler : AuthorizationHandler<RoleURLRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleURLRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
