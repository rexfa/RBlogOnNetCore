using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using Microsoft.AspNetCore.Mvc.ViewComponents;

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
        public IViewComponentResult Invoke(int userId)
        {
            // async Task<IViewComponentResult> 
            //ContentViewComponentResult
            //IViewComponentResult
            //HtmlContentViewComponentResult
            //http://www.cnblogs.com/sanshi/p/7750497.html
            //http://www.cnblogs.com/shenba/p/6629212.html

            //var blogs = _blogRepository.Table.OrderByDescending(b => b.releasedOn).TakeLast(10);
            List<Blog> blogs = null;
            try
            {
                blogs = _blogRepository.Table.OrderByDescending(b => b.releasedOn).TakeLast(10).ToList();
            }
            catch (Exception ex)
            {
                return View("NoData",ex.Message);
            }
            if (blogs.Count>0)
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
                return View(model);
            }
            else
            {
                return View("NoData","0数据");

            }
        }
    }
}
