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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RBlogOnNetCore.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        private readonly MysqlContext _context;
        private IHostingEnvironment _env;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Picture> _pictureRepository;

        public PictureController(MysqlContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._pictureRepository = new EfRepository<Picture>(this._context);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PicList()
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                int customerId = int.Parse(CustomerId);

            }
        }
        public async Task<IActionResult> UploadPicture()
        {
            string callback = Request.Query["CKEditorFuncNum"];//要求返回值
            var upload = Request.Form.Files[0];
            string tpl = "<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");</script>";
            if (upload == null)
                return Content(string.Format(tpl, "", callback, "请选择一张图片！"), "text/html");
            //判断是否是图片类型
            List<string> imgtypelist = new List<string> { "image/pjpeg", "image/png", "image/x-png", "image/gif", "image/bmp" };
            if (imgtypelist.FindIndex(x => x == upload.ContentType) == -1)
            {
                return Content(string.Format(tpl, "", callback, "请上传一张图片！"), "text/html");
            }
            var data = Request.Form.Files["upload"];
            string filepath = _env.WebRootPath + "\\userfile\\images";
            //string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(upload.FileName);
            string imgname = upload.FileName;
            string fullpath = Path.Combine(filepath, imgname);
            try
            {
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                if (data != null)
                {
                    await Task.Run(() =>
                    {
                        using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                        {
                            //data.CopyTo(fs);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                return Content(string.Format(tpl, "", callback, "图片上传失败：" + ex.Message), "text/html");
            }
            return Content(string.Format(tpl, "/userfile/images/" + imgname, callback, ""), "text/html");
        }
    }
}