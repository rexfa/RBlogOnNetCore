using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using RBlogOnNetCore.Utils;

namespace RBlogOnNetCore.Handles
{
    public class LoginHandle
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        public LoginHandle(MysqlContext context)
        {
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
        }
        public virtual bool LoginByPassword(string username, string password)
        {

            var customer =  _customerRepository.Table.Where(u => u.name == username).First();
            if (customer != null)
            {
                string pwd_org = customer.password;
                string pwd_input = SecurityTools.MD5Hash(password + customer.salt);
                if (pwd_input == pwd_org)
                {
                    SignIn(customer, true);
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
        public virtual void SignIn(Customer customer, bool createPersistentCookie)
        {
            var nowUtc = DateTime.UtcNow;
            List<Claim> customerClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, customer.name),
                new Claim(ClaimTypes.Sid, customer.id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(customerClaims,"Basic");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
            {
                IssuedUtc = nowUtc,
                ExpiresUtc = nowUtc.AddDays(30)
            };
            var ticket = new AuthenticationTicket(claimsPrincipal, "Basic");

        }
    }
}
