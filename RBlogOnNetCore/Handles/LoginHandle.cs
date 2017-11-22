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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RBlogOnNetCore.Handles
{
    public class LoginHandle
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly HttpContext _httpContext;
        public LoginHandle(MysqlContext context, HttpContext httpContext)
        {
            this._context = context;
            this._httpContext = httpContext;
            this._customerRepository = new EfRepository<Customer>(this._context);
        }
        public virtual async Task<bool> LoginByPassword(string username, string password)
        {

            var customer =  _customerRepository.Table.Where(u => u.name == username).First();
            if (customer != null)
            {
                string pwd_org = customer.password;
                string pwd_input = SecurityTools.MD5Hash(password + customer.salt);
                if (pwd_input == pwd_org)
                {
                    await Login(customer, true);
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
        public virtual async Task<bool> Login(Customer customer, bool createPersistentCookie)
        {
            var nowUtc = DateTime.UtcNow;
            List<Claim> customerClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, customer.name),
                new Claim(ClaimTypes.Sid, customer.id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(customerClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
            {
                IssuedUtc = nowUtc,
                ExpiresUtc = nowUtc.AddDays(30)
            };
            var ticket = new AuthenticationTicket(claimsPrincipal, CookieAuthenticationDefaults.AuthenticationScheme);
            //JsonHelper jh = new JsonHelper();

            await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal);
            return true;
        }
        public virtual async void Logout()
        {
            await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
