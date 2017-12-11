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
        public LocalFileTools()
        {

        }

        /// <summary>
        /// 获取指定文件的扩展名 例:  .txt
        /// </summary>
        /// <param name="fileName">指定文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileExtName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName.IndexOf('.') <= 0)
                return "";

            fileName = fileName.ToLower().Trim();


            return fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.'));
        }

    }
}
