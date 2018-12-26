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
using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICustomerService _customerService;

        public AccountController(MysqlContext context,ICustomerService customerService)
        {
            _context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._customerService = customerService;
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

                LoginHandle loginHandle = new LoginHandle(_context, this.HttpContext, _customerService);
                bool isLogin = await loginHandle.LoginByPassword(model);

                if (isLogin)
                {
                    var user = HttpContext.User;
                    return Redirect("/blog/add");
                }
            }
            return View();
        }
        [HttpGet("logout")]
        public ActionResult Logout()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                LoginHandle loginHandle = new LoginHandle(_context, this.HttpContext, _customerService);
                loginHandle.Logout();
            }
            return View();
        }
        [HttpGet("denied")]
        public ActionResult Denied()
        {
            return View();
        }
    }
}