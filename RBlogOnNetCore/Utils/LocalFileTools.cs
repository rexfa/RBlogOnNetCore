using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RBlogOnNetCore.Utils
{
    public class LocalFileTools
    {
        /*
         * appsettings
            "LocalDir": {
            "PictureLocalDir": "/images/contents/"
            },
         */
        private readonly string pictureLocalDir;
        public LocalFileTools(IConfiguration configuration)
        {
            string pictureLocalDir = configuration.GetSection("LocalDir")["PictureLocalDir"];
        }

        public string GetPictureURL(string picName,int customerId)
        {
            string localString = PictureLocalDir+customerId.ToString()+"/";
            string url = localString + picName;
            return url;

        }
        public string PictureLocalDir
        {
            get
            {
                return pictureLocalDir; 
            }
        }
    }
}
