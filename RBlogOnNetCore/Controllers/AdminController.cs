using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RBlogOnNetCore.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMemCacheService _memCacheService;
        public AdminController(IMemCacheService memCacheService)
        {
            _memCacheService = memCacheService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CacheManager()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CacheManager(string command)
        {
            if (command == "ClearAll")
            {
                _memCacheService.ClearMemCache();
                ViewData["result"] = "缓存全部清理完毕。";
            }
            return View();
        }
        public IActionResult BannerManger()
        {
            return View();
        }

    }
}