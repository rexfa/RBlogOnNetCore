using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;
using RBlogOnNetCore.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RBlogOnNetCore.Controllers
{
    public class CommentController : Controller
    {
        private readonly INormalCommentService _normalCommentService;
        public CommentController(INormalCommentService normalCommentService)
        {
            this._normalCommentService = normalCommentService;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddApi(string nikename,string email,string homepageUrl,string commentText,int blogId)
        {
            if (string.IsNullOrEmpty(nikename) || string.IsNullOrEmpty(email)|| string.IsNullOrEmpty(commentText))
            {
                return new JsonResult("提交失败，格式可能不正确");
            }
            else if (!email.Contains("@")&& !email.Contains("."))
            {
                return new JsonResult("提交失败，Email格式可能不正确");
            }
            else
            {
                NormalComment normalComment = new NormalComment()
                {
                    BlogId = blogId,
                    CreatedOn = DateTime.Now,
                    CommentText = commentText,
                    Email = email,
                    Nikename = nikename,
                    IsDeleted = false,
                    HomepageUrl = homepageUrl == null ? "" : homepageUrl,
                    PreIds = ""
                };
                normalComment = _normalCommentService.CreateNormalComment(normalComment);
                string n = nikename;
                return new JsonResult("Succuss");
            }
        }
    }
}
