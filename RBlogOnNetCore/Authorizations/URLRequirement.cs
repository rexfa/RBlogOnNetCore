using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Authorizations
{
    public class URLRequirement : IAuthorizationRequirement
    {
        public URLRequirement(string urlName)
        {
            URLName = urlName;
        }
        public string URLName { get; set; }
    }
}
