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
    public class BlogController : Controller
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Blog> _blogRepository;
        private readonly ITagService _tagService;
        private readonly IBlogService _blogService;

        public BlogController(MysqlContext context,ITagService tagService,IBlogService blogService)
        {
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._blogRepository = new EfRepository<Blog>(this._context);
            this._tagService = tagService;
            this._blogService = blogService;
            //var services = new ServiceCollection();
            //var provider = services.BuildServiceProvider();
            //this._tagService = provider.GetService<ITagService>();
            var ts = _tagService.GetBlogTags(1);
            if (ts == null)
                return;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            this.HttpContext.Response.Headers.Add("cache-control", new[] { "public,no-cache" });
            this.HttpContext.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123        }
            return View();
        }
        [Authorize]
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
                model.CustomerId = int.Parse(CustomerId);
                var blog = _blogService.InsertBlog(model);
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
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var blog = _blogRepository.Table.Where(b => b.Id == id).First();
            if (blog == null)
                return View();
            var tagList = _tagService.GetBlogTags(blog.Id);
            string tags = string.Join(",", tagList.Select(t => { return t.TagName; }).ToArray());
            var model = new BlogModel()
            {
                Content = blog.Content,
                CreatedOn = blog.CreatedOn,
                CustomerId = blog.CustomerId,
                CustomerName = "",
                Id = blog.Id,
                ReleasedOn = blog.ReleasedOn,
                Title = blog.Title,
                Tags = tags
            };
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(BlogModel model)
        {
            if (model == null)
            {
                return View();
            }
            int Id = model.Id;
            var blog = _blogRepository.Table.Where(b => b.Id == Id).FirstOrDefault();
            if (blog == null)
                return View();
            blog.Content = model.Content;
            blog.Title = model.Title;
            _tagService.SetTagsToBlog(Id, model.Tags);
            _blogRepository.Update(blog);
            return View(model);
        }
        [HttpGet,HttpPost]
        public ActionResult List(int TagId,int index)
        {
            //http://www.cnblogs.com/sanshi/p/7750497.html

            var tag = _tagService.GetTagById(TagId);
            ViewData["tagName"] = tag.TagName;
            int id = 0;
            bool editable = false;
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                id = int.Parse(CustomerId);
                
                if (id == 1)
                {
                    editable = true;
                }
            }
            index = index == 0 ? 0 : index - 1;
            var model = _blogService.GetPagedBlogsByTagId(TagId, index, 5, id);
            model.Editable = editable;
            if (model.Blogs == null)
                return Content("还没有博客");
            if(model.TotalItems<=0)
                return Content("还没有博客");
            return View(model);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var blog = _blogRepository.Table.Where(b => b.Id == id).FirstOrDefault();
            if (blog == null)
                return View();
            var model = new BlogModel()
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                CreatedOn = blog.CreatedOn,
                ReleasedOn = blog.ReleasedOn,
                Tags =string.Join(",",_tagService.GetBlogTags(id).Select(t=> { return t.TagName; }).ToList())
            };
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                model.Editable = true;
            }
            return View(model);
        }
    }
}