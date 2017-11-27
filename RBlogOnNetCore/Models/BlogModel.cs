using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class BlogModel
    {
        public int id { get; set; }
        public string tilte { set; get; }
        public string content { set; get; }
        public string createdOnString { get; set; }
        public string releasedOnString { get; set; }
        public string customerName { set; get; }
    }
}
