using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Services;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace RBlogOnNetCore.ViewComponents
{
    public class TagListViewComponent : ViewComponent
    {
        private readonly MysqlContext _context;
        private readonly IRepository<Tag> _tagRepository;
        private readonly TagService _tagService;
        public TagListViewComponent(MysqlContext context, TagService tagService)
        {
            this._context = context;
            this._tagRepository = new EfRepository<Tag>(this._context);
            this._tagService = tagService;
        }
        public IViewComponentResult Invoke()
        {
            var hotTags = _tagService.GetHotTags();
            List<TagModel> tms = new List<TagModel>();
            foreach (var t in hotTags)
            {
                TagModel tm = new TagModel()
                {
                    Id = t.Id,
                    TagName = t.TagName
                };
                tms.Add(tm);
            }
            return View(tms);

        }
    }
}
