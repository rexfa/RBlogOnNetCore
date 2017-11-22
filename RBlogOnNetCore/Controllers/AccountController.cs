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

namespace RBlogOnNetCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly MysqlContext _context;

        public AccountController(MysqlContext context)
        {
            _context = context;
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
                //HttpContext.Session.Set("s",)
                //var result =  await 
                LoginHandle loginHandle = new LoginHandle(_context, this.HttpContext);
                if (await loginHandle.LoginByPassword(model.Name, model.password))
                {
                    return Redirect("/blog/add");
                }
            }
            return View();
        }
 
        public ActionResult Logout()
        {
            return View();
        }
    }
}