using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.Authorizations
{
    public class URLRoleAuthorizationHandler : AuthorizationHandler<URLRequirement>
    {
        private readonly ICustomerService _customerService;
        public URLRoleAuthorizationHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, URLRequirement requirement)
        {
            if (context.User != null)
            {
                if (context.User.IsInRole("sysadmin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //_customerService
                }
            }
            return Task.CompletedTask;
        }
    }
}
