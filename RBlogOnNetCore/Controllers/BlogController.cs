using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Handles;
using RBlogOnNetCore.Utils;
using RBlogOnNetCore.Services;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace RBlogOnNetCore.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly ITagService _tagService;

        public BlogController(MysqlContext context)
        {
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._blogRepository = new EfRepository<Blog>(this._context);
            var services = new ServiceCollection();
            var provider = services.BuildServiceProvider();
            this._tagService = provider.GetService<ITagService>();
            var ts = _tagService.GetBlogTags(1);
            if (ts == null)
                return;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            this.HttpContext.Response.Headers.Add("cache-control", new[] { "public,no-cache" });
            this.HttpContext.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123        }
            return View();
        }
        [HttpPost]
        public ActionResult Add(BlogModel model)
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                string content = Request.Form["content"].ToString();
                var now = DateTime.Now;
                //用户Id
                var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                var blog = new Blog()
                {
                    Title = model.Title,
                    CustomerId = int.Parse(CustomerId),
                    Content = model.Content,
                    CreatedOn = now,
                    UpdatedOn = now,
                    ReleasedOn = now,
                    IsDeleted = false,
                    IsReleased = true
                };
                _blogRepository.Insert(blog);
                _context.SaveChanges();
                model.Id = blog.Id;
                model.CreatedOn = blog.CreatedOn;
                model.ReleasedOn = blog.ReleasedOn;
            }
            else
            {
                Redirect("/login");
            }            
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(BlogModel model)
        {
            return View();
        }
        [HttpGet,HttpPost]
        public ActionResult BlogList()
        {
            //http://www.cnblogs.com/sanshi/p/7750497.html
            List<Blog> blogs = _blogRepository.Table.OrderByDescending(b => b.ReleasedOn).TakeLast(10).ToList();
            //if (blogs.Count > 0)
            //{
            //    BasePageableModel model = new BasePageableModel();
            //    model.Blogs = new List<BlogModel>();
            //    foreach (Blog b in blogs)
            //    {
            //        var blogModel = new BlogModel()
            //        {
            //            id = b.id,
            //            content = b.content,
            //            createdOn = b.createdOn,
            //            releasedOn = b.releasedOn,
            //            customerName = b.Customer.name
            //        };
            //        model.Blogs.Add(blogModel);
            //    }
            //    return PartialView("_BlogList", model);
            //}
            //else
            //{
            //    return Content("还没有博客");
            //}
            return Content("还没有博客");
        }
    }
}