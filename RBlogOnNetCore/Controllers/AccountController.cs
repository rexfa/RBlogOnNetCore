﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Models;
using Microsoft.AspNetCore.Authorization;

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
                var result =  await 
            }
            return View();
        }
 
        public ActionResult Logout()
        {
            return View();
        }
    }
}