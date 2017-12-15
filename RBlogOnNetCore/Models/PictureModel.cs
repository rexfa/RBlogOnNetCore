using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class PictureModel
    {
        public int Id { get; set; }
        public string CustomName { get; set; }
        public string Url { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
