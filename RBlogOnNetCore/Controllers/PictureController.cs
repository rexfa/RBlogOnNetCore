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
using RBlogOnNetCore.Models;
using RBlogOnNetCore.Utils;
using RBlogOnNetCore.Configuration;
using Microsoft.Extensions.Options;

namespace RBlogOnNetCore.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        private readonly MysqlContext _context;
        private IHostingEnvironment _env;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Picture> _pictureRepository;

        private readonly LocalDir _localDir;

        public PictureController(MysqlContext context, IHostingEnvironment env, IOptions<LocalDir> option)
        {
            _context = context;
            _env = env;
            this._customerRepository = new EfRepository<Customer>(this._context);
            this._pictureRepository = new EfRepository<Picture>(this._context);
            _localDir = option.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 用户图片列表
        /// </summary>
        /// <returns></returns>
        public IActionResult CustomerPicList()
        {
            var isAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                int customerId = int.Parse(CustomerId);
                var customer = _customerRepository.GetById(customerId);
                try
                {
                    //Core到现在还不支持延迟加载所以手写
                    //List<Picture> pictures = customer.Pictures.Where(x => x.isDeleted == false).OrderByDescending(x => x.updatedOn).ToList();
                    List<Picture> pictures = _pictureRepository.Table.Where(x=>x.customerId == customerId)
                        .OrderByDescending(x => x.updatedOn).ToList();

                    PictureListModel picList = new PictureListModel();
                    picList.pictures = new List<PictureModel>();
                    foreach (var p in pictures)
                    {
                        var picmodel = new PictureModel()
                        {
                            id = p.id,
                            customName = p.customName,
                            url = _localDir.PictureUrlDir + p.localName
                        };
                        picList.pictures.Add(picmodel);
                    }
                    return View(picList);
                }
                catch (Exception ex)
                {
                    return View(null);
                }
                
                
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> UploadPicture()
        {
            var CustomerId = HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
            int customerId = int.Parse(CustomerId);

            string callback = Request.Query["CKEditorFuncNum"];//要求返回值
            var data = Request.Form.Files["pic"];
            string tpl = "<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");</script>";
            if (data == null)
                return Content(string.Format(tpl, "", callback, "请选择一张图片！"), "text/html");
                    
            //判断是否是图片类型
            List<string> imgtypelist = new List<string> { "image/pjpeg","image/jpeg", "image/png", "image/x-png", "image/gif", "image/bmp" };
            if (imgtypelist.FindIndex(x => x == data.ContentType) == -1)
            {
                return Content(string.Format(tpl, "", callback, "请上传一张图片！"), "text/html");
            }
            DateTime now = DateTime.Now;
            string customName = Request.Form["customName"];
            if (string.IsNullOrEmpty(customName))
            {
                customName = string.Format("{0:F}", now); 
            }

            string filepath = Directory.GetCurrentDirectory() + _localDir.PictureLocalDir;
            //string imgname = Utils.GetOrderNum() + Utils.GetFileExtName(upload.FileName);
            string imgname = data.FileName;
            
            try
            {
                if (!Directory.Exists(filepath))
                    Directory.CreateDirectory(filepath);
                Picture pic = new Picture()
                {
                    customerId = customerId,
                    customName = customName,
                    isDeleted = false,
                    originalName = imgname,
                    picType = data.ContentType,
                    updatedOn = now,
                    localName = Guid.NewGuid().ToString("N") + LocalFileTools.GetFileExtName(imgname)

                };
                _pictureRepository.Insert(pic);
                _context.SaveChanges();
                string fullpath = Path.Combine(filepath, pic.localName);
                if (data != null)
                {
                    await Task.Run(() =>
                    {
                        using (FileStream fs = new FileStream(fullpath, FileMode.Create))
                        {
                            data.CopyTo(fs);//写硬盘
                        }
                    });
                }
                return Redirect("\\Picture\\CustomerPicView\\" + pic.id);
            }
            catch (Exception ex)
            {
                return Content(string.Format(tpl, "", callback, "图片上传失败：" + ex.Message), "text/html");
            }
        }
        [HttpGet]
        public IActionResult CustomerPicView(int id)
        {
            var pic = _pictureRepository.GetById(id);
            PictureModel model = new PictureModel()
            {
                id = pic.id,
                customName = pic.customName,
                url = _localDir.PictureUrlDir+pic.localName,
                updatedOn = pic.updatedOn
            };
            return View(model);
        }
    }
}