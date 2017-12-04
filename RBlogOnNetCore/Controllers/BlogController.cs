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
using System.Security.Claims;

namespace RBlogOnNetCore.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Blog> _blogRepository;

        public BlogController(MysqlContext context)
        {
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._blogRepository = new EfRepository<Blog>(this._context);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
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
                    title = model.title,
                    customerId = int.Parse(CustomerId),
                    content = model.content,
                    createdOn = now,
                    updatedOn = now,
                    releasedOn = now,
                    isDeleted = false,
                    isReleased = true
                };
                _blogRepository.Insert(blog);
                _context.SaveChanges();
                model.id = blog.id;
                model.createdOnString = blog.createdOn.ToString();
                model.releasedOnString = blog.releasedOn.ToString();
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
            List<Blog> blogs = _blogRepository.Table.OrderByDescending(b => b.releasedOn).TakeLast(10).ToList();
            if (blogs.Count > 0)
            {
                BlogListModel model = new BlogListModel();
                model.Blogs = new List<BlogModel>();
                foreach (Blog b in blogs)
                {
                    var blogModel = new BlogModel()
                    {
                        id = b.id,
                        content = b.content,
                        createdOnString = b.createdOn.ToString("yyyy-MM-dd HH:mm:ss"),
                        releasedOnString = b.releasedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                        customerName = b.Customer.name
                    };
                    model.Blogs.Add(blogModel);
                }
                return PartialView("_BlogList", model);
            }
            else
            {
                return Content("还没有博客");
            }
        }
    }
}