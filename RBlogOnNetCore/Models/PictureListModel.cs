using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class PictureListModel
    {
        public List<PictureModel> Pictures { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

    }
}
