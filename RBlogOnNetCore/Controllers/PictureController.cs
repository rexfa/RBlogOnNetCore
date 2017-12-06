using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using Microsoft.AspNetCore.Hosting;

namespace RBlogOnNetCore.Controllers
{
    public class PictureController : Controller
    {
        private readonly MysqlContext _context;
        private IHostingEnvironment _env;

        public PictureController(MysqlContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}