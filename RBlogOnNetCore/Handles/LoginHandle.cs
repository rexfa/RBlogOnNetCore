﻿using System;
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
        public virtual async bool LoginByPassword(string username, string password)
        {

            var customer =  _customerRepository.Table.Where(u => u.name == username).First();
            if (customer != null)
            {
                string pwd_org = customer.password;
                string pwd_input = SecurityTools.MD5Hash(password + customer.salt);
                if (pwd_input == pwd_org)
                {
                    Login(customer, true);
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
        public virtual async void Login(Customer customer, bool createPersistentCookie)
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
            //JsonHelper jh = new JsonHelper();

            await _httpContext.SignInAsync(claimsPrincipal);
        }
        public virtual async void Logout()
        {
            await _httpContext.SignOutAsync();
        }
    }
}
