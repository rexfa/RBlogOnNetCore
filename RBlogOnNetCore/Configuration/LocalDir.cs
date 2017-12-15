using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Configuration
{
    /*
     *  "LocalDir": {
     *  "PictureLocalDir": "/wwwroot/images/contents/",
     *  "PictureUrlDir": "/images/contents/"
     *  },
     * */
    public class LocalDir
    {
        public string PictureLocalDir { get; set; }
        public string PictureUrlDir { get; set; }
    }
}
