using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Configuration
{
    /*
 *  "PageSettings": {
 *  "PageSize": "5",
 *  "ShowPage": "2"
 *  },
 * */
    public class PageSettings
    {
        public int PageSize { get; set; }
        public int ShowPage { get; set; }
    }
}
