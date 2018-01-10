using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using RBlogOnNetCore.Utils;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authentication.Cookies;
using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.Handles
{
    public class LoginHandle
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly HttpContext _httpContext;
        private readonly ICustomerService _customerService;
        public LoginHandle(MysqlContext context, HttpContext httpContext,ICustomerService customerService)
        {
            this._context = context;
            this._httpContext = httpContext;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._customerService = customerService;
        }
        public virtual async Task<bool> LoginByPassword(LoginModel model)
        {

            var customer =  _customerRepository.Table.Where(u => u.Name == model.Name).First();
            if (customer != null)
            {
                string pwd_org = customer.Password;
                string pwd_input = SecurityTools.MD5Hash(model.password + customer.Salt);
                if (pwd_input == pwd_org)
                {
                    await Login(customer, model, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public virtual async Task<bool> Login(Customer customer, LoginModel model,bool createPersistentCookie)
        {
            if (customer != null)
            {
                string pwd_org = customer.Password;
                string pwd_input = SecurityTools.MD5Hash(model.password + customer.Salt);
                var roles = _customerService.GetCustomerRoles(customer);
                if (pwd_input == pwd_org)
                {
                    List<Claim> customerClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,  customer.Id.ToString()),
                        new Claim(ClaimTypes.Name, customer.Name),
                        new Claim(ClaimTypes.Sid, customer.Id.ToString())
                    };
                    foreach (var r in roles)
                    {
                        customerClaims.Add(new Claim(ClaimTypes.Role, r.RoleName));
                    }

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(customerClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(45),//登录过期分钟数量
                        IsPersistent = createPersistentCookie,
                        AllowRefresh = false
                    });

                    var user = _httpContext.User;
                    return true;
                }
            }
            return false;
        }
        public virtual async void Logout()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
