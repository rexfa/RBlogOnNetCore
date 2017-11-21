using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace RBlogOnNetCore.Middleware
{
    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermission(this IApplicationBuilder builder, PermissionMiddlewareOption option)
        {
            return builder.UseMiddleware<PermissionMiddleware>(option);
        }
    }
}
