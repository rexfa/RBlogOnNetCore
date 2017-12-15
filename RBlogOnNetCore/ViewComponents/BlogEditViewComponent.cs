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
    public class BlogEditViewComponent:ViewComponent
    {
        //https://github.com/xoxco/jQuery-Tags-Input 考虑加入这个插件
        private readonly MysqlContext _context;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Blog> _blogRepository;
        public BlogEditViewComponent(MysqlContext context)
        {
            this._context = context;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._blogRepository = new EfRepository<Blog>(this._context);
        }
        public IViewComponentResult Invoke(BlogModel model)
        {
            if (model == null)
            {
                return View();
            }
            else
            {
                return View(model);
            }
        }
    }
}
