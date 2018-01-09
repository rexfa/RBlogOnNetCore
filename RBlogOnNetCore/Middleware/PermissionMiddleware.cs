using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.Middleware
{
    public class PermissionMiddleware
    {
        /// <summary>
        /// 中间件管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 配置
        /// </summary>
        private readonly PermissionMiddlewareOption _option;
        /// <summary>
        /// 用户权限集合
        /// </summary>
        internal static List<UserPermission> _userPermissions;

        private readonly ICustomerService _customerService;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next">管道代理对象</param>
        /// <param name="permissionResitory">权限仓储对象</param>
        /// <param name="option">权限中间件配置选项</param>
        public PermissionMiddleware(RequestDelegate next, PermissionMiddlewareOption option, ICustomerService customerService)
        {
            _option = option;
            _next = next;
            _userPermissions = option.UserPerssions;
            _customerService = customerService;
        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            //请求url
            var questUrl = context.Request.Path.Value.ToLower();
            //是否已经验证
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                int customerId = int.Parse(context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value);
                var customer = _customerService.GetCustomerById(customerId);
                var roles = _customerService.GetCustomerRoles(customer);
                foreach (var r in roles)
                {
                    if (_customerService.IsAdmin(r))
                    {
                        return this._next(context);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        context.Response.Redirect(_option.NoPermissionAction);
                    }
                }
                if (_userPermissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    //用户名
                    var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name).Value;
                    if (_userPermissions.Where(w => w.UserName == userName && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        return this._next(context);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        context.Response.Redirect(_option.NoPermissionAction);
                    }
                }
            }
            return this._next(context);
        }
}
}
