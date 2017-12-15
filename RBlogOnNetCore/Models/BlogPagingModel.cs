using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.Infrastructures;

namespace RBlogOnNetCore.Models
{
    public class BlogPagingModel : BasePageableModel ,IPageableModel
    {
        public List<BlogModel> Blogs { get; set; }
        //public int PageSize { get; set; }
        public int Index { get; set; }
        public string SearchKeyword { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public bool Editable { get; set; }
    }
}
