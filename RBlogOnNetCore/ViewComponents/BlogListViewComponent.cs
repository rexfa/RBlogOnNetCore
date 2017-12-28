﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Security.Claims;
using RBlogOnNetCore.Infrastructures;

namespace RBlogOnNetCore.ViewComponents
{
    public class BlogListViewComponent: ViewComponent
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Blog> _blogRepository;
        public BlogListViewComponent(MysqlContext context)
        {
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._blogRepository = new EfRepository<Blog>(this._context);
        }
        public IViewComponentResult Invoke(List<Blog> blogs)
        {
            // async Task<IViewComponentResult> 
            //ContentViewComponentResult
            //IViewComponentResult
            //HtmlContentViewComponentResult
            //http://www.cnblogs.com/sanshi/p/7750497.html
            //http://www.cnblogs.com/shenba/p/6629212.html

            //var blogs = _blogRepository.Table.OrderByDescending(b => b.releasedOn).TakeLast(10);
            if (blogs == null)
            {
                try
                {
                    blogs = _blogRepository.Table.OrderByDescending(b => b.ReleasedOn).Take(10).ToList();
                }
                catch (Exception ex)
                {
                    return View("NoData", ex.Message);
                }
            }
            if (blogs.Count>0)
            {
                var cs = _customerRepository.Table.FirstOrDefault();//执行过一次就可以在以下b.Customer里呼出了，奇怪
                BlogPagingModel model = new BlogPagingModel();
                IPagedList<Blog> pagedBlogs = new PagedList<Blog>(blogs, 0, 10);
                PrepareBlogPagingModel(model,pagedBlogs);
                //model.Blogs = new List<BlogModel>();
                //foreach (Blog b in blogs)
                //{
                //    var blogModel = new BlogModel()
                //    {
                //        Id = b.Id,
                //        Title = b.Title,
                //        Content = b.Content,
                //        CreatedOn = b.CreatedOn,
                //        ReleasedOn = b.ReleasedOn,
                //        CustomerName = b.Customer.CustomerName
                //    };
                //    model.Blogs.Add(blogModel);
                //}
                //var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                //if (isAuthenticated)
                //{
                //    var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                //    int id = int.Parse(CustomerId);
                //    if (id == 1)
                //    {
                //        model.Editable = true;
                //    }
                //}
                return View(model);
            }
            else
            {
                return View("NoData","0数据");

            }
        }
        [NonAction]
        protected virtual IPagedList<Blog> GetBlogs(int pageIndex, int pageSize, bool showNotReleased = false )
        {
            try
            {
                var query = _blogRepository.Table;
                if (!showNotReleased)
                    query = query.Where(b=>b.IsReleased == true);
                query = query.Where(b => b.IsDeleted == false);
                query = query.OrderByDescending(b => b.ReleasedOn);
                var blogs = new PagedList<Blog>(query, pageIndex, pageSize);
                return blogs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [NonAction]
        protected virtual void PrepareBlogModel(BlogModel model,Blog blog)
        {
            if (model == null)
                throw new Exception("model is null!");
            model.Id = blog.Id;
            model.Content = blog.Content;
            model.CreatedOn = blog.CreatedOn;
            model.CustomerId = blog.CustomerId;
            model.CustomerName = blog.Customer.CustomerName;
            model.ReleasedOn = blog.ReleasedOn;
            model.Title = blog.Title;
        }
        [NonAction]
        protected virtual void PrepareBlogPagingModel(BlogPagingModel model, IPagedList<Blog> blogs, int pageIndex = 0)
        {
            if (model == null)
                throw new Exception("model is null!");
            if (blogs == null)
            { 
                blogs = GetBlogs(pageIndex, 10);
            }
            model.TotalPages = blogs.TotalPages;
            model.Index = blogs.PageIndex;
            model.PageSize = blogs.PageSize;
            model.TotalPages = blogs.TotalCount;
            model.Blogs = model.Blogs ?? new List<BlogModel>();
            foreach (var blog in blogs)
            {
                var m = new BlogModel();
                PrepareBlogModel(m, blog);
                model.Blogs.Add(m);
            }
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                int id = int.Parse(CustomerId);
                if (id == 1)
                {
                    model.Editable = true;
                }
            }
        }
    }
}
