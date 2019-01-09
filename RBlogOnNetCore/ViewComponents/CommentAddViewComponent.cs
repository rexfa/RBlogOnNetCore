using Microsoft.AspNetCore.Mvc;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using RBlogOnNetCore.EF.Domain;

namespace RBlogOnNetCore.ViewComponents
{
    public class CommentAddViewComponent : ViewComponent
    {
        private readonly MysqlContext _context;
        private readonly IRepository<NormalComment> _normalCommentRepository;
        public CommentAddViewComponent(MysqlContext mysqlContext)
        {
            _context = mysqlContext;
            _normalCommentRepository = new EfRepository<NormalComment>(this._context);
        }
        public IViewComponentResult Invoke()
        {
            NormalCommentModel model = new NormalCommentModel();
            return View(model);
        }
    }
}
