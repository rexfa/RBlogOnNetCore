using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBlogOnNetCore.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public int Sort { get; set; }
        public string Color { get; set; }
    }
}
