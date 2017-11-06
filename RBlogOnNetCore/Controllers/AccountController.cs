using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Models;

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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            return View();
        }
 
        public ActionResult Logout()
        {
            return View();
        }
    }
}