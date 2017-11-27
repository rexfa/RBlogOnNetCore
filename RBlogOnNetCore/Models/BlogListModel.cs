using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class BlogListModel
    {
        public List<BlogModel> Blogs { get; set; }
        public int pageSize { get; set; }
        public int index { get; set; }
        public string searchKeyword { get; set; }
        public string beginTime { get; set; }
        public string endTime { get; set; }
    }
}
