using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.Authorizations
{
    public class RoleURLAuthorizationHandler : AuthorizationHandler<RoleURLRequirement>
    {
        private readonly ICustomerService _customerService;
        public RoleURLAuthorizationHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleURLRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
