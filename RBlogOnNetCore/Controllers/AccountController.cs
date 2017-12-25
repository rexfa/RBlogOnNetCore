using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using RBlogOnNetCore.Handles;
using RBlogOnNetCore.Utils;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace RBlogOnNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;

        public AccountController(MysqlContext context)
        {
            _context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                //LoginHandle loginHandle = new LoginHandle(_context, this.HttpContext);
                //bool isLogin = await loginHandle.LoginByPassword(model.Name, model.password);
                //
                var customer = _customerRepository.Table.Where(u => u.Name == model.Name).First();
                if (customer != null)
                {
                    string pwd_org = customer.Password;
                    string pwd_input = SecurityTools.MD5Hash(model.password + customer.Salt);
                    if (pwd_input == pwd_org)
                    {
                        List<Claim> customerClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, customer.Name),
                            new Claim(ClaimTypes.Sid, customer.Id.ToString()),
                            new Claim(ClaimTypes.Role, "Admin")
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(customerClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(45),//登录过期分钟数量
                            IsPersistent = false,
                            AllowRefresh = false
                        });
                        var user = HttpContext.User;
                        return Redirect("/blog/add");
                    }
                 }
                 //

                //    if (isLogin)
                //{
                //    var user = HttpContext.User;
                //    return Redirect("/blog/add");
                //}
            }
            return View();
        }
 
        public ActionResult Logout()
        {
            return View();
        }
        [HttpGet("denied")]
        public ActionResult Denied()
        {
            return View();
        }
    }
}