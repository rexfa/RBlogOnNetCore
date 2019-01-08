using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using RBlogOnNetCore.Models;

using RBlogOnNetCore.Services;

namespace RBlogOnNetCore.ViewComponents
{
    public class CommentListViewComponent : ViewComponent
    {
        private readonly INormalCommentService _normalCommentService;
        public CommentListViewComponent(INormalCommentService normalCommentService)
        {
            this._normalCommentService = normalCommentService;
        }
        public IViewComponentResult Invoke(int blogId=0)
        {
            var cns = _normalCommentService.GetNormalComments(blogId, 10);
            if (cns == null)
                return View("NoData");
            List<NormalCommentModel> model = new List<NormalCommentModel>();
            foreach (var cn in cns)
            {
                NormalCommentModel m = new NormalCommentModel()
                {
                    Id = cn.Id,
                    BlogId = cn.BlogId,
                    CommentText = cn.CommentText,
                    CreatedOnStr = cn.CreatedOn.ToLocalTime().ToLongDateString(),
                    Email = cn.Email,
                    HomepageUrl = cn.HomepageUrl,
                    IsDeleted = cn.IsDeleted,
                    Nikename = cn.Nikename,
                    PreIds = cn.PreIds
                };
                model.Add(m);
            }
            return View(model);

        }

    }
}
