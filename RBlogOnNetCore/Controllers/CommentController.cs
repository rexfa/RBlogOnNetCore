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
        private readonly IPostTokenManagerService _postTokenManagerService;
        public CommentController(INormalCommentService normalCommentService, IPostTokenManagerService postTokenManagerService)
        {
            this._normalCommentService = normalCommentService;
            this._postTokenManagerService = postTokenManagerService;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            NormalCommentModel model = new NormalCommentModel()
            {
                PostToken = _postTokenManagerService.GetNewPostToken(DateTime.Now.ToShortTimeString()),
                ServerMsg = "谢谢访问"
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(NormalCommentModel model)
        {
            bool check = _postTokenManagerService.CheckAndDelPostToken(model.PostToken);
            if (check)
            {
                NormalComment normalComment = new NormalComment()
                {
                    BlogId = model.BlogId,
                    CreatedOn = DateTime.Now,
                    CommentText = model.CommentText,
                    Email = model.Email,
                    Nikename = model.Nikename,
                    IsDeleted = false,
                    HomepageUrl = string.IsNullOrEmpty(model.HomepageUrl) ? "" : model.HomepageUrl,
                    PreIds = ""
                };
                normalComment = _normalCommentService.CreateNormalComment(normalComment);
            }
            model = new NormalCommentModel()
            {
                ServerMsg = check?"提交成功":"Token丢失"    
            };
            return View(model);
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
