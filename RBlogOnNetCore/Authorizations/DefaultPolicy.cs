using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.Authorizations
{
    public class DefaultPolicy : AuthorizationPolicy
    {
        public DefaultPolicy(IEnumerable<IAuthorizationRequirement> requirements,
            IEnumerable<string> authenticationSchemes) : base(requirements, authenticationSchemes)
        {

        }
    }
}
